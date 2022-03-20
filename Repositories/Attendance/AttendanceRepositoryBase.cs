
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class AttendanceEntity
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
        public string MealName { get; set; }
        public int ChildId { get; set; }
    }

    public class AttendanceRepositoryBase : RepositoryBase
    {
        public AttendanceRepositoryBase(RepositoryContext context) : base(context) { }
        protected IQueryable<AttendanceEntry> JoinAttendance(IQueryable<Child> children, IQueryable<ChildAttendance> childAttendance, IQueryable<GroupAttendance> groupAttendance)
        {
            var result = from _children in children
                         join _childAttendance in childAttendance on _children.Id equals _childAttendance.ChildId
                         join _attendance in groupAttendance on new { _childAttendance.MealId, _childAttendance.Date, _children.GroupId } equals new
                         { _attendance.MealId, _attendance.Date, _attendance.GroupId } into ga
                         from _groupAttendance in ga.DefaultIfEmpty()
                         join _schema in _context.Set<MealSchema>().AsNoTracking() on _childAttendance.MealId equals _schema.Id
                         select new AttendanceEntry
                         {
                             MealName = _schema.Name,
                             Present = _childAttendance.Attendance,
                             Masked = _groupAttendance == null ? false : _groupAttendance.Attendance,
                             ChildId = _children.Id,
                             Year = _childAttendance.Date.Year,
                             Month = _childAttendance.Date.Month,
                             Day = _childAttendance.Date.Day
                         };
            return result;
        }
        protected IQueryable<AttendanceEntry> JoinAttendance(IQueryable<Child> children, IQueryable<ChildAttendance> attendance)
        {
            var result = from _children in children
                         join _attendance in attendance on _children.Id equals _attendance.ChildId
                         join _schema in _context.Set<MealSchema>().AsNoTracking() on _attendance.MealId equals _schema.Id
                         select new AttendanceEntry
                         {
                             MealName = _schema.Name,
                             Present = _attendance.Attendance,
                             Masked = false,
                             ChildId = _children.Id,
                             Year = _attendance.Date.Year,
                             Month = _attendance.Date.Month,
                             Day = _attendance.Date.Day
                         };
            return result;
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
            if (day == 0)
            {
                return c => c.Date.Year == year && c.Date.Month == month;
            }
            return c => c.Date == new DateTime(year, month, day);
        }
    }

}