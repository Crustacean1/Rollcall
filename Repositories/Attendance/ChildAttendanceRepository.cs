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
        public IEnumerable<NamedAttendanceEntity> GetAttendance(Child? target, int year, int month, int day = 0)
        {
            var childAttendance = GetSetWhere(ChildAttendanceCondition(target)).Where(ChildDateCondition(year, month, day));
            var result = childAttendance.Select(q => new AttendanceEntity
            {
                MealId = q.MealId,
                Present = q.Attendance ? 1 : 0,
                Date = new MealDate { Year = year, Month = month, Day = q.Date.Day }
            });
            return AppendNames(result);
        }
        public IEnumerable<NamedAttendanceEntity> GetMonthlySummary(Child target, int year, int month)
        {
            var children = GetSetWhere(ChildCondition(target));
            var childAttendance = GetSetWhere(ChildDateCondition(year, month, 0)).Where(ChildAttendanceCondition(target));
            var groupAttendance = GetSetWhere(GroupDateCondition(year, month, 0)).Where(GroupAttendanceCondition(target));
            var attendance = JoinAttendance(JoinAttendance(children, childAttendance), groupAttendance);

            var result = attendance.GroupBy(a => a.MealId)
            .Select(a => new AttendanceEntity
            {
                MealId = a.Key,
                Present = a.Count(m => m.Present && !m.Masked),
                Date = new MealDate { Year = year, Month = month, Day = 0 }
            });
            return AppendNames(result);
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
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private Expression<Func<GroupAttendance, bool>> GroupAttendanceCondition(Child? child)
        {
            if (child == null)
            {
                return c => true;
            }
            return c => c.GroupId == child.GroupId;
        }
        private Expression<Func<ChildAttendance, bool>> ChildAttendanceCondition(Child? child)
        {
            if (child == null)
            {
                return c => true;
            }
            return c => c.ChildId == child.Id;
        }
        private Expression<Func<Child, bool>> ChildCondition(Child? child)
        {
            if (child == null)
            {
                return c => true;
            }
            return c => c.Id == child.Id;
        }
    }
}