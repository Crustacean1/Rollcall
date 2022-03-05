
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{
    public class AttendanceEntry
    {
        public int ChildId { get; set; }
        public string Name { get; set; }
        public bool Masked { get; set; }
        public int Attendance { get; set; }
        public MealDate Date { get; set; }
    }
    public class AttendanceEntity
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public int MealId { get; set; }
        public int ChildId { get; set; }
        public int GroupId { get; set; }

        public bool Masked { get; set; }
        public bool Attendance { get; set; }
        public string MealName { get; set; }
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
        protected IEnumerable<AttendanceEntry> GetAttendanceQuery<Q>(Expression<Func<AttendanceEntity, bool>> whereCondition,
        Expression<Func<AttendanceEntity, Q>> selector,
        Func<Q, MealDate> dateRetriever,
        Func<Q, int> targetRetriever,
        Func<Q, string> nameRetriever)
        {
            var result = _context.Set<ChildAttendance>().AsNoTracking()
            .Join(_context.Set<GroupAttendance>().AsNoTracking().DefaultIfEmpty(), c => new { c.MealId, c.Date, c.TargetChild.GroupId },
             g => new { g.MealId, g.Date, g.GroupId },
             (childAttendance, groupAttendance) => new AttendanceEntity
             {
                 MealId = childAttendance.MealId,
                 ChildId = childAttendance.ChildId,
                 GroupId = groupAttendance.GroupId,

                 Attendance = childAttendance.Attendance,
                 Masked = groupAttendance?.Attendance ?? false,

                 Year = childAttendance.Date.Year,
                 Month = childAttendance.Date.Month,
                 Day = childAttendance.Date.Day
             })
            .Join(_context.Set<MealSchema>().AsNoTracking(), c => c.MealId, s => s.Id,
            (attendance, mealSchema) => new AttendanceEntity
            {
                MealId = attendance.MealId,
                ChildId = attendance.ChildId,
                GroupId = attendance.GroupId,

                Year = attendance.Year,
                Month = attendance.Month,
                Day = attendance.Day,

                Attendance = attendance.Attendance,
                Masked = attendance.Masked,
                MealName = mealSchema.Name
            })

            .Where(whereCondition)
            .GroupBy(selector)
            .Select(a => new AttendanceEntry
            {
                ChildId = targetRetriever(a.Key),
                Attendance = a.Count(a => a.Attendance && a.Masked != null && !a.Masked),
                Name = nameRetriever(a.Key),
                Masked = a.Count(a => a.Masked) != 0,
                Date = dateRetriever(a.Key)
            });
            return result;
        }
    }
}