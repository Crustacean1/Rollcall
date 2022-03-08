
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{
    public class AttendanceEntity
    {
        public string Name { get; set; }
        public int Attendance { get; set; }
        public MealDate Date { get; set; }
    }

    public class AttendanceEntry
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public bool Masked { get; set; }
        public bool Attendance { get; set; }
        public string MealName { get; set; }
    }

    public class AttendanceRepositoryBase
    {
        protected readonly RepositoryContext _context;
        public AttendanceRepositoryBase(RepositoryContext context)
        {
            _context = context;
        }
        protected IEnumerable<AttendanceEntity> GetAttendanceQuery(Expression<Func<ChildAttendance, bool>> targetCondition,
        Expression<Func<AttendanceEntry, bool>> dateCondition)
        {
            var result = _context.Set<ChildAttendance>().AsNoTracking()
                        .Where(targetCondition)
                        .Join(_context.Set<MealSchema>().AsNoTracking(), c => c.MealId, s => s.Id, (c, s) => new AttendanceEntry
                        {
                            Year = c.Date.Year,
                            Month = c.Date.Month,
                            Day = c.Date.Day,
                            Attendance = c.Attendance,
                            MealName = s.Name,
                            Masked = false
                        })
                        .Where(dateCondition)
                        .Select(a => new AttendanceEntity
                        {
                            Attendance = a.Attendance ? 1 : 0,
                            Date = new MealDate { Year = a.Year, Month = a.Month, Day = a.Day },
                            Name = a.MealName
                        });
            return result;
        }
        protected IEnumerable<AttendanceEntity> GetSummaryQuery<Q>(Expression<Func<Child, bool>> childCondition,
        Expression<Func<AttendanceEntry, bool>> dateCondition,
        Expression<Func<AttendanceEntry, Q>> grouper,
        Func<Q, MealDate> getDate,
        Func<Q, string> getName)
        {
            var fullAttendance = from children in _context.Set<Child>().AsNoTracking().Where(a => a.Id == 1)
                                 join childAttendance in _context.Set<ChildAttendance>().AsNoTracking() on children.Id equals childAttendance.ChildId
                                 join sparseGroupAttendance in _context.Set<GroupAttendance>().AsNoTracking() on
                                    new { children.GroupId, childAttendance.Date, childAttendance.MealId }
                                    equals
                                    new { sparseGroupAttendance.GroupId, sparseGroupAttendance.Date, sparseGroupAttendance.MealId }
                                    into ga
                                 from groupAttendance in ga.DefaultIfEmpty()
                                 join mealSchema in _context.Set<MealSchema>().AsNoTracking() on childAttendance.MealId equals mealSchema.Id
                                 select new AttendanceEntry
                                 {
                                     Masked = groupAttendance == null ? false : groupAttendance.Attendance,
                                     Attendance = childAttendance.Attendance,
                                     MealName = mealSchema.Name,
                                     Year = childAttendance.Date.Year,
                                     Month = childAttendance.Date.Month,
                                     Day = childAttendance.Date.Day,
                                 };
            var result = fullAttendance.Where(dateCondition)
            .GroupBy(grouper)
            .Select(a => new AttendanceEntity
            {
                Attendance = a.Count(a => a.Attendance && !a.Masked),
                Date = getDate(a.Key),
                Name = getName(a.Key)
            });
            return result;
        }
    }
}