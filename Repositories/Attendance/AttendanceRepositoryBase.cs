
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class AttendanceEntity
    {
        public string Name { get; set; }
        public int Attendance { get; set; }
        public MealDate Date { get; set; }
        public int ChildId { get; set; }
    }

    public class AttendanceEntry
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public bool Masked { get; set; }
        public bool Attendance { get; set; }
        public string MealName { get; set; }
        public int ChildId { get; set; }
    }

    public class AttendanceRepositoryBase
    {
        protected readonly RepositoryContext _context;

        public AttendanceRepositoryBase(RepositoryContext context)
        {
            _context = context;
        }
        protected IQueryable<AttendanceEntry> GetAttendanceQuery(Expression<Func<ChildAttendance, bool>> targetCondition,
        Expression<Func<ChildAttendance,bool>> dateCondition)
        {
            var result = _context.Set<ChildAttendance>().AsNoTracking()
                        .Where(targetCondition)
                        .Where(dateCondition)
                        .Join(_context.Set<MealSchema>().AsNoTracking(), c => c.MealId, s => s.Id, (c, s) => new AttendanceEntry
                        {
                            Year = c.Date.Year,
                            Month = c.Date.Month,
                            Day = c.Date.Day,
                            Attendance = c.Attendance,
                            MealName = s.Name,
                            Masked = false,
                            ChildId = c.ChildId
                        });
            return result;
        }
        protected IQueryable<AttendanceEntry> GetUnmaskedSummaryQuery(Expression<Func<Child, bool>> childCondition, Expression<Func<AttendanceEntry, bool>> dateCondition)
        {
            var fullAttendance = from children in _context.Set<Child>().AsNoTracking().Where(childCondition)
                                 join childAttendance in _context.Set<ChildAttendance>().AsNoTracking() on children.Id equals childAttendance.ChildId
                                 join mealSchema in _context.Set<MealSchema>().AsNoTracking() on childAttendance.MealId equals mealSchema.Id
                                 select new AttendanceEntry
                                 {
                                     Masked = false,
                                     Attendance = childAttendance.Attendance,
                                     MealName = mealSchema.Name,
                                     Year = childAttendance.Date.Year,
                                     Month = childAttendance.Date.Month,
                                     Day = childAttendance.Date.Day,
                                 };
            return fullAttendance.Where(dateCondition);
        }
        protected IQueryable<AttendanceEntry> GetMaskedSummaryQuery(Expression<Func<Child, bool>> childCondition, Expression<Func<AttendanceEntry, bool>> dateCondition)
        {
            var fullAttendance = from children in _context.Set<Child>().AsNoTracking().Where(childCondition)
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
            return fullAttendance.Where(dateCondition);
        }
    }
}