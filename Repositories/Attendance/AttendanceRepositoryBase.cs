
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{
    public class AttendanceEntry
    {
        public int ChildId { get; set; }
        public MealDate Date { get; set; }
        public string Name { get; set; }
        public bool Masked { get; set; }
        public int Attendance { get; set; }
    }

    public class AttendanceKey<Q, T>
    {
        public string Name { get; set; }
        public Q q { get; set; }
        public T t { get; set; }
    }
    public class AttendanceRepositoryBase
    {
        protected readonly RepositoryContext _context;
        public AttendanceRepositoryBase(RepositoryContext context)
        {
            _context = context;
        }
        protected IEnumerable<AttendanceEntry> GetAttendanceQuery<Q, T>(Func<ChildAttendance, bool> whereCondition,
        Func<DateTime, Q> dateSelector,
        Func<Child, T> targetSelector,
        Func<Q, MealDate> dateRetriever,
        Func<T, int> targetRetriever)
        {
            var result = _context.Set<ChildAttendance>().AsNoTracking().Select(a => a)

            .Join(_context.Set<GroupAttendance>().AsNoTracking(), c => new { c.MealId, c.Date, c.TargetChild.GroupId },
             g => new { g.MealId, g.Date, g.GroupId },
             (childAttendance, groupAttendance) => new { childAttendance, groupAttendance })

            .Join(_context.Set<MealSchema>().AsNoTracking(), c => c.childAttendance.MealId, s => s.Id,
            (attendance, mealSchema) => new { attendance, mealSchema })

            .Where(a => whereCondition(a.attendance.childAttendance))

            .GroupBy(a => new AttendanceKey<Q, T>
            {
                Name = a.mealSchema.Name,
                t = targetSelector(a.attendance.childAttendance.TargetChild),
                q = dateSelector(a.attendance.childAttendance.Date)
            })
            .Select(a => new AttendanceEntry
            {
                ChildId = targetRetriever(a.Key.t),
                Attendance = a.Count(a => a.attendance.childAttendance.Attendance && a.attendance.groupAttendance.Attendance),
                Date = dateRetriever(a.Key.q),
                Name = a.Key.Name,
                Masked = a.Any(a => a.attendance.groupAttendance.Attendance)
            });
            return result;
        }
    }
}