using System.Linq.Expressions;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class GroupAttendanceRepository : MonthlyAttendanceRepository
    {
        public GroupAttendanceRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<AttendanceEntity> GetAttendance(Group? target, bool masked, int year, int month)
        {
            var attendance = masked ?
            GetMaskedQuery(target, year, month, 0) :
            GetUnmaskedQuery(target, year, month, 0);

            var result = attendance.GroupBy(e => new { e.Day, e.MealName })
            .Select(d => new AttendanceEntity
            {
                Present = d.Count(q => !q.Masked),
                Name = d.Key.MealName,
                Date = new MealDate { Year = year, Month = month, Day = d.Key.Day }
            });
            return result;
        }
        public IEnumerable<AttendanceEntity> GetSummary(Group? target, bool masked, int year, int month, int day = 0)
        {
            var attendance = masked ?
            GetMaskedQuery(target, year, month, day) :
            GetUnmaskedQuery(target, year, month, day);

            var result = attendance.GroupBy(a => a.MealName)
            .Select(a => new AttendanceEntity
            {
                Present = a.Count(q => !q.Masked),
                Name = a.Key,
                Date = new MealDate { Year = year, Month = month, Day = 0 }
            });
            return result;
        }
        public bool SetGroupAttendance(Group? target, string mealName, bool present, MealDate date)
        {
            if (target == null)
            {
                return SetAttendance(_context.Set<Group>().ToList(), mealName, present, date);
            }
            return SetAttendance(target, mealName, present, date);
        }
        private bool SetAttendance(IEnumerable<Group> targets, string mealName, bool present, MealDate date)
        {
            var result = new List<bool>();
            foreach (var target in targets)
            {
                result.Add(SetAttendance(target, mealName, present, date));
            }
            return result.All(r => r == true);
        }
        private bool SetAttendance(Group target, string mealName, bool present, MealDate date)
        {
            var attendance = _context.Set<GroupAttendance>()
            .Where(c => c.MealName == mealName && c.Date == new DateTime(date.Year, date.Month, date.Day) && c.GroupId == target.Id)
            .FirstOrDefault();
            if (attendance != null)
            {
                attendance.Attendance = present;
            }
            else
            {
                attendance = new GroupAttendance
                {
                    MealName = mealName,
                    Date = new DateTime(date.Year, date.Month, date.Day),
                    GroupId = target.Id,
                    Attendance = present
                };
                _context.Set<GroupAttendance>().Add(attendance);
            }
            return attendance.Attendance;
        }
        private IQueryable<AttendanceEntry> GetUnmaskedQuery(Group? target, int year, int month, int day)
        {
            var children = GetSetWhere(TargetCondition(target));

            var childAttendance = GetSetWhere(ChildDateCondition(year, month, day))
            .Where(a => a.Attendance);

            var attendance = JoinAttendance(children, childAttendance);
            return attendance;
        }
        private IQueryable<AttendanceEntry> GetMaskedQuery(Group? target, int year, int month, int day)
        {
            var children = GetSetWhere(TargetCondition(target));

            var childAttendance = GetSetWhere(ChildDateCondition(year, month, day))
            .Where(a => a.Attendance);

            var groupAttendance = GetSetWhere(GroupDateCondition(year, month, day))
            .Where(GroupAttendanceCondition(target))
            .Where(a => a.Attendance);

            var attendance = JoinAttendance(children, childAttendance, groupAttendance);
            return attendance;
        }
        private Expression<Func<Child, bool>> TargetCondition(Group? group)
        {
            if (group == null)
            {
                return c => true;
            }
            return c => c.GroupId == group.Id;
        }
        private Expression<Func<GroupAttendance, bool>> GroupAttendanceCondition(Group? group)
        {
            if (group == null)
            {
                return g => true;
            }
            return g => g.GroupId == group.Id;
        }
    }
}