using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{

    public class ChildAttendanceRepository : AttendanceRepositoryBase, IAttendanceRepository<Child>
    {
        private readonly ILogger<ChildAttendanceRepository> _logger;
        public ChildAttendanceRepository(RepositoryContext context, ILogger<ChildAttendanceRepository> logger) : base(context)
        {
            _logger = logger;
        }
        public IEnumerable<AttendanceEntity> GetMonthlyAttendance(Child target, int year, int month)
        {
            Expression<Func<ChildAttendance, bool>> targetCondition = c => c.ChildId == target.Id;
            Expression<Func<AttendanceEntry, bool>> dateCondition = a => a.Year == year && a.Month == month;
            var result = GetAttendanceQuery(targetCondition, dateCondition);
            return result;
        }
        public IEnumerable<AttendanceEntity> GetMonthlySummary(Child target, int year, int month)
        {
            Expression<Func<AttendanceEntry, bool>> dateCondition = (e) => (e.Year == year && e.Month == month);
            Expression<Func<Child, bool>> childCondition = (e) => (e.Id == target.Id);
            var childAttendance = GetUnmaskedSummary(
                childCondition,
                dateCondition,
                c => new { c.MealName },
                c => new MealDate { Year = year, Month = month, Day = 0 },
                c => c.MealName
            );

            return childAttendance;
        }
        public IEnumerable<AttendanceEntity> GetAttendance(Child target, int year, int month, int day)
        {
            Expression<Func<ChildAttendance, bool>> targetCondition = c => c.ChildId == target.Id;
            Expression<Func<AttendanceEntry, bool>> dateCondition = a => a.Year == year && a.Month == month && a.Day == day;
            var result = GetAttendanceQuery(targetCondition, dateCondition);
            return result;
        }
        public async Task SetAttendance(Child target, int mealId, bool attendance, int year, int month, int day)
        {
            var previousAttendance = _context.Set<ChildAttendance>()
            .Where(c => c.ChildId == target.Id && c.Date == new DateTime(year, month, day) && c.MealId == mealId)
            .FirstOrDefault();
            if (previousAttendance != null)
            {
                previousAttendance.Attendance = attendance;
            }
            else
            {
                _context.ChildAttendance.Add(new ChildAttendance
                {
                    Date = new DateTime(year, month, day),
                    Attendance = attendance,
                    ChildId = target.Id,
                    MealId = mealId
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}