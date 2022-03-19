using System.Linq.Expressions;
using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{
    public class GroupAttendanceRepository : AttendanceRepositoryBase
    {
        public GroupAttendanceRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<NamedAttendanceEntity> GetAttendance(Group? target, int year, int month, int day = 0)
        {
            var children = GetSetWhere(TargetCondition(target));
            var childAttendance = GetSetWhere(ChildDateCondition(year, month, day));
            var attendance = JoinAttendance(children, childAttendance);
            var result = attendance.GroupBy(e => new { e.Day, e.MealId })
            .Select(d => new AttendanceEntity
            {
                MealId = d.Key.MealId,
                Date = new MealDate { Year = year, Month = month, Day = d.Key.Day },
                Present = d.Count(a => a.Present)
            });
            return AppendNames(result);
        }
        public IEnumerable<NamedAttendanceEntity> GetMonthlySummary(Group? target, int year, int month)
        {
            var children = GetSetWhere(TargetCondition(target));
            var childAttendance = GetSetWhere(ChildDateCondition(year, month, 0));
            var groupAttendance = GetSetWhere(GroupDateCondition(year, month, 0));
            var attendance = JoinAttendance(JoinAttendance(children, childAttendance), groupAttendance);

            var result = attendance.GroupBy(a => new { a.Day, a.MealId })
            .Select(a => new AttendanceEntity
            {
                Present = a.Count(m => m.Present && !m.Masked),
                MealId = a.Key.MealId,
                Date = new MealDate { Year = year, Month = month, Day = a.Key.Day }
            });
            return AppendNames(result);
        }
        public bool SetAttendance(Group target, int mealId, bool present, int year, int month, int day)
        {
            var attendance = _context.Set<GroupAttendance>()
            .Where(c => c.MealId == mealId && c.Date == new DateTime(year, month, day) && c.GroupId == target.Id)
            .FirstOrDefault();
            if (attendance != null)
            {
                attendance.Attendance = present;
            }
            else
            {
                attendance = new GroupAttendance
                {
                    MealId = mealId,
                    Date = new DateTime(year, month, day),
                    GroupId = target.Id,
                    Attendance = present
                };
                _context.Set<GroupAttendance>().Add(attendance);
            }
            return attendance.Attendance;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        private Expression<Func<Child, bool>> TargetCondition(Group? group)
        {
            if (group == null)
            {
                return c => true;
            }
            return c => c.GroupId == group.Id;
        }
    }
}