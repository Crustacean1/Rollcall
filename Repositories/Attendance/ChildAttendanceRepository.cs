using Rollcall.Models;

namespace Rollcall.Repositories
{

    public class ChildAttendanceRepository : MonthlyAttendanceRepository
    {
        private readonly ILogger<ChildAttendanceRepository> _logger;
        public ChildAttendanceRepository(RepositoryContext context, ILogger<ChildAttendanceRepository> logger) : base(context)
        {
            _logger = logger;
        }
        public IEnumerable<AttendanceEntity> GetAttendance(Child child, int year, int month)
        {
            var childAttendance = GetSetWhere<ChildAttendance>(c => c.ChildId == child.Id)
            .Where(a => a.Date.Year == year && a.Date.Month == month);

            var result = childAttendance.Join(_context.Set<MealSchema>(), c => c.MealId, s => s.Id, (e, s) => new AttendanceEntity
            {
                Name = s.Name,
                Present = e.Attendance ? 1 : 0,
                Date = new MealDate { Year = year, Month = month, Day = e.Date.Day }
            });
            return result;
        }
        public IEnumerable<AttendanceEntity> GetMonthlyCount(Child child, int year, int month)
        {
            var children = GetSetWhere<Child>(c => c.Id == child.Id);

            var childAttendance = GetSetWhere<ChildAttendance>(a => a.Date.Year == year && a.Date.Month == month)
            .Where(c => c.ChildId == child.Id)
            .Where(a => a.Attendance);

            var groupAttendance = GetSetWhere<GroupAttendance>(a => a.Date.Year == year && a.Date.Month == month)
            .Where(c => c.GroupId == child.GroupId)
            .Where(a => a.Attendance);

            var attendance = JoinAttendance(children, childAttendance, groupAttendance);

            var result = attendance
            .GroupBy(a => a.MealName)
            .Select(a => new AttendanceEntity
            {
                Name = a.Key,
                Present = a.Count(q => !q.Masked),
                Date = new MealDate { Year = year, Month = month, Day = 0 }
            });
            return result;
        }
        public int ExtendAttendance(IEnumerable<Child> children, int year, int month)
        {
            int successfullUpdates = 0;
            foreach (var child in children)
            {
                if (GetSetWhere<ChildAttendance>(c => c.ChildId == child.Id).FirstOrDefault() == null)
                {
                    ExtendAttendance(child, year, month);
                    ++successfullUpdates;
                }
            }
            return successfullUpdates;
        }
        private bool ExtendAttendance(Child child, int year, int month)
        {
            var defaultAttendance = child.DefaultMeals;
            if (defaultAttendance == null) { return false; }

            var newAttendances = new List<ChildAttendance>();
            for (var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday) { continue; }
                foreach (var meal in defaultAttendance)
                {
                    newAttendances.Add(new ChildAttendance
                    {
                        ChildId = child.Id,
                        MealId = meal.MealId,
                        Attendance = meal.Attendance,
                        Date = new DateTime(date.Year, date.Month, date.Day)
                    });
                }
            }
            _context.Set<ChildAttendance>().AddRange(newAttendances);
            return true;
        }

        public bool SetAttendance(Child target, int mealId, bool present, int year, int month, int day)
        {
            var attendance = _context.Set<ChildAttendance>()
            .Where(c => c.ChildId == target.Id && c.Date == new DateTime(year, month, day) && c.MealId == mealId)
            .FirstOrDefault();
            if (attendance != null)
            {
                attendance.Attendance = present;
            }
            else
            {
                attendance = new ChildAttendance
                {
                    Date = new DateTime(year, month, day),
                    Attendance = present,
                    ChildId = target.Id,
                    MealId = mealId
                };
                _context.ChildAttendance.Add(attendance);
            }
            return attendance.Attendance;
        }
    }
}