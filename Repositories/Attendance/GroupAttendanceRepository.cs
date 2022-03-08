using System.Linq.Expressions;
using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{
    public class GroupAttendanceRepository : AttendanceRepositoryBase, IAttendanceRepository<Group>
    {
        public GroupAttendanceRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<AttendanceEntity> GetMonthlyAttendance(Group target, int year, int month)
        {
            Expression<Func<Child, bool>> groupCondition = (c) => c.GroupId == target.Id;
            Expression<Func<AttendanceEntry, bool>> dateCondition = a => a.Year == year && a.Month == month;
            var result = GetUnmaskedSummary(groupCondition,
            dateCondition,
            c => new { c.Day, c.MealName },
            c => new MealDate { Year = year, Month = month, Day = c.Day },
            c => c.MealName);
            return result;
        }
        public IEnumerable<AttendanceEntity> GetMonthlySummary(Group target, int year, int month)
        {
            Expression<Func<Child, bool>> groupCondition = c => c.GroupId == target.Id;
            Expression<Func<AttendanceEntry, bool>> dateCondition = c => c.Year == year && c.Month == month;
            var result = GetMaskedSummary(groupCondition,
            dateCondition,
            c => c.MealName,
            c => new MealDate { Year = year, Month = month, Day = 0 },
            c => c);
            return result;
        }
        public IEnumerable<AttendanceEntity> GetAttendance(Group target, int year, int month, int day)
        {
            Expression<Func<Child, bool>> groupCondition = c => c.GroupId == target.Id;
            Expression<Func<AttendanceEntry, bool>> dateCondition = c => c.Year == year && c.Month == month && c.Day == day;
            var result = GetUnmaskedSummary(groupCondition,
            dateCondition,
            c => c.MealName,
            c => new MealDate { Year = year, Month = month, Day = day },
            c => c);
            return result;
        }
        public async Task SetAttendance(Group target, int mealId, bool attendance, int year, int month, int day)
        {
            var previousAttendance = _context.Set<GroupAttendance>()
            .Where(c => c.MealId == mealId && c.Date == new DateTime(year, month, day) && c.GroupId == target.Id)
            .FirstOrDefault();
            if (previousAttendance != null)
            {
                previousAttendance.Attendance = attendance;
            }
            else
            {
                _context.Set<GroupAttendance>().Add(new GroupAttendance
                {
                    MealId = mealId,
                    Date = new DateTime(year, month, day),
                    GroupId = target.Id,
                    Attendance = attendance
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}