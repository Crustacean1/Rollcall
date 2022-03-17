using System.Linq.Expressions;
using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{
    public class GroupAttendanceRepository : AttendanceRepositoryBase
    {
        public GroupAttendanceRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<AttendanceEntity> GetMonthlyAttendance(Group? target, int year, int month)
        {
            Expression<Func<AttendanceEntry, bool>> dateCondition = a => a.Year == year && a.Month == month;

            var result = GetUnmaskedSummaryQuery(TargetCondition(target),
            dateCondition)
            .GroupBy(a => new { a.Day, a.MealName })
            .Select(a => new AttendanceEntity
            {
                Attendance = a.Count(m => m.Attendance),
                Name = a.Key.MealName,
                Date = new MealDate { Year = year, Month = month, Day = a.Key.Day }
            });
            return result;
        }
        public IEnumerable<AttendanceEntity> GetMonthlySummary(Group? target, int year, int month)
        {
            Expression<Func<AttendanceEntry, bool>> dateCondition = c => c.Year == year && c.Month == month;

            var result = GetMaskedSummaryQuery(TargetCondition(target), dateCondition)
            .GroupBy(a => a.MealName)
            .Select(a => new AttendanceEntity
            {
                Attendance = a.Count(a => a.Attendance && !a.Masked),
                Name = a.Key,
                Date = new MealDate { Year = year, Month = month, Day = 0 }
            });
            return result;
        }
        public IEnumerable<AttendanceEntity> GetDailySummary(Group? target, int year, int month, int day)
        {
            Expression<Func<AttendanceEntry, bool>> dateCondition = c => c.Year == year && c.Month == month && c.Day == day;

            var result = GetUnmaskedSummaryQuery(TargetCondition(target), dateCondition)
            .GroupBy(a => a.MealName)
            .Select(a => new AttendanceEntity
            {
                Attendance = a.Count(m => m.Attendance),
                Name = a.Key,
                Date = new MealDate { Year = year, Month = month, Day = day }
            });
            return result;
        }
        public IEnumerable<IEnumerable<AttendanceEntity>> GetDailyAttendance(Group? target, int year, int month, int day)
        {
            Expression<Func<Child, bool>> groupCondition = c => c.GroupId == target.Id;
            Expression<Func<AttendanceEntry, bool>> dateCondition = a => a.Year == year && a.Month == month && a.Day == day;
            var result = GetUnmaskedSummaryQuery(TargetCondition(target), dateCondition)
            .GroupBy(a => a.MealName)
            .Select(a => a.Select(b => new AttendanceEntity
            {
                Attendance = b.Attendance ? 1 : 0,
                Date = new MealDate { Year = year, Month = month, Day = day },
                Name = b.MealName
            }));
            return result;
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