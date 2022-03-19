
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class AttendanceEntity
    {
        public int MealId { get; set; }
        public int Present { get; set; }
        public MealDate Date { get; set; }
        public int ChildId { get; set; }
    }
    public class NamedAttendanceEntity
    {
        public string Name { get; set; }
        public int Present { get; set; }
        public MealDate Date { get; set; }
        public int ChildId { get; set; }
    }

    public class AttendanceEntry
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public bool Masked { get; set; }
        public bool Present { get; set; }
        public int MealId { get; set; }
        public int GroupId { get; set; }
    }

    public class AttendanceRepositoryBase : RepositoryBase
    {
        public AttendanceRepositoryBase(RepositoryContext context) : base(context) { }
        protected IQueryable<NamedAttendanceEntity> AppendNames(IQueryable<AttendanceEntity> entries)
        {
            return entries.Join(_context.Set<MealSchema>().AsNoTracking(), e => e.MealId, s => s.Id, (e, s) => new NamedAttendanceEntity
            {
                Name = s.Name,
                Present = e.Present,
                Date = e.Date,
                ChildId = e.ChildId
            });
        }
        protected IQueryable<AttendanceEntry> JoinAttendance(IQueryable<AttendanceEntry> children, IQueryable<GroupAttendance> groupAttendance)
        {
            return children.
            GroupJoin(groupAttendance, c => c.GroupId, a => a.GroupId, (entries, attendance) => new { entries, attendance })
            .SelectMany(a => a.attendance.DefaultIfEmpty(), (a, c) => new AttendanceEntry
            {
                MealId = a.entries.MealId,

                Year = a.entries.Year,
                Month = a.entries.Month,
                Day = a.entries.Day,

                Present = a.entries.Present,
                Masked = c == null ? false : c.Attendance
            });
        }
        protected IQueryable<AttendanceEntry> JoinAttendance(IQueryable<Child> children, IQueryable<ChildAttendance> attendance)
        {
            return children.Join(attendance, c => c.Id, a => a.ChildId, (c, a) => new AttendanceEntry
            {
                MealId = a.MealId,
                GroupId = c.GroupId,

                Masked = false,
                Present = a.Attendance,

                Year = a.Date.Year,
                Month = a.Date.Month,
                Day = a.Date.Day
            });
        }
        protected Expression<Func<GroupAttendance, bool>> GroupDateCondition(int year, int month, int day)
        {
            if (day == 0)
            {
                return c => c.Date.Year == year && c.Date.Month == month;
            }
            return c => c.Date == new DateTime(year, month, day);
        }
        protected Expression<Func<ChildAttendance, bool>> ChildDateCondition(int year, int month, int day)
        {
                return c => c.Date.Year == year && c.Date.Month == month;
            if (day == 0)
            {
                return c => c.Date.Year == year && c.Date.Month == month;
            }
            return c => c.Date == new DateTime(year, month, day);
        }
    }

}