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
        public IEnumerable<AttendanceEntity> GetAttendance(Child? target, int year, int month, int day = 0)
        {
            var childAttendance = GetSetWhere<ChildAttendance>(ChildAttendanceCondition(target)).Where(ChildDateCondition(year, month, day));
            var result = childAttendance.Join(_context.Set<MealSchema>(), c => c.MealId, s => s.Id, (e, s) => new AttendanceEntity
            {
                Name = s.Name,
                Present = e.Attendance ? 1 : 0,
                Date = new MealDate { Year = year, Month = month, Day = e.Date.Day }
            });
            return result;
        }
        public IEnumerable<AttendanceEntity> GetMonthlySummary(Child target, int year, int month)
        {
            var children = GetSetWhere(ChildCondition(target));
            var childAttendance = GetSetWhere(ChildDateCondition(year, month, 0))
            .Where(ChildAttendanceCondition(target))
            .Where(a => a.Attendance);
            var groupAttendance = GetSetWhere(GroupDateCondition(year, month, 0))
            .Where(GroupAttendanceCondition(target))
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