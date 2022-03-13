using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{

    public class ChildAttendanceRepository : AttendanceRepositoryBase
    {
        private readonly ILogger<ChildAttendanceRepository> _logger;
        public ChildAttendanceRepository(RepositoryContext context, ILogger<ChildAttendanceRepository> logger) : base(context)
        {
            _logger = logger;
        }
        public IEnumerable<AttendanceEntity> GetMonthlyAttendance(Child? target, int year, int month)
        {
            Expression<Func<ChildAttendance, bool>> dateCondition = a => a.Date.Year == year && a.Date.Month == month;
            var result = GetAttendanceQuery(TargetCondition(target), dateCondition)
            .Select(a => new AttendanceEntity
            {
                Attendance = a.Attendance ? 1 : 0,
                Name = a.MealName,
                Date = new MealDate { Year = year, Month = month, Day = a.Day },
            });
            return result;
        }
        public IEnumerable<AttendanceEntity> GetMonthlySummary(Child target, int year, int month)
        {
            Expression<Func<AttendanceEntry, bool>> dateCondition = (e) => (e.Year == year && e.Month == month);
            Expression<Func<Child, bool>> childCondition = c => c.Id == target.Id;

            var childAttendance = GetMaskedSummaryQuery(childCondition, dateCondition);

            return childAttendance.GroupBy(a => a.MealName)
            .Select(a => new AttendanceEntity
            {
                Date = new MealDate { Year = year, Month = month, Day = 0 },
                Attendance = a.Count(m => m.Attendance&&!m.Masked),
                Name = a.Key
            });
        }
        public IEnumerable<AttendanceEntity> GetAttendance(Child? target, int year, int month, int day)
        {
            Expression<Func<ChildAttendance, bool>> dateCondition = a => a.Date.Year == year && a.Date.Month == month && a.Date.Day == day;
            var result = GetAttendanceQuery(TargetCondition(target), dateCondition)
            .Select(a => new AttendanceEntity
            {
                Attendance = a.Attendance ? 1 : 0,
                Date = new MealDate { Year = year, Month = month, Day = day },
                Name = a.MealName
            });
            return result;
        }
        public async Task<bool> SetAttendance(Child target, int mealId, bool present, int year, int month, int day)
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
            await _context.SaveChangesAsync();
            return attendance.Attendance;
        }

        private Expression<Func<ChildAttendance, bool>> TargetCondition(Child? child)
        {
            if (child == null)
            {
                return c => true;
            }
            return c => c.ChildId == child.Id;
        }
    }
}