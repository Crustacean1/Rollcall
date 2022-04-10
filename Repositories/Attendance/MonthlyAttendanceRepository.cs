using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
namespace Rollcall.Repositories
{
    public class MonthlyAttendanceRepository : AttendanceRepositoryBase
    {
        public MonthlyAttendanceRepository(RepositoryContext context) : base(context) { }

        protected IQueryable<AttendanceEntry> JoinAttendance(IQueryable<Child> children, IQueryable<ChildAttendance> childAttendance, IQueryable<GroupAttendance> groupAttendance)
        {
            var result = from _children in children
                         join _childAttendance in childAttendance on _children.Id equals _childAttendance.ChildId
                         join _attendance in groupAttendance on new { _childAttendance.MealName, _childAttendance.Date, _children.GroupId } equals new
                         { _attendance.MealName, _attendance.Date, _attendance.GroupId } into ga
                         from _groupAttendance in ga.DefaultIfEmpty()
                         select new AttendanceEntry
                         {
                             MealName = _childAttendance.MealName,
                             Present = _childAttendance.Attendance,
                             Masked = _groupAttendance == null ? false : _groupAttendance.Attendance,
                             ChildId = _children.Id,
                             Day = _childAttendance.Date.Day
                         };
            return result;
        }
        protected IQueryable<AttendanceEntry> JoinAttendance(IQueryable<Child> children, IQueryable<ChildAttendance> attendance)
        {
            var result = from _children in children
                         join _attendance in attendance on _children.Id equals _attendance.ChildId
                         select new AttendanceEntry
                         {
                             MealName = _attendance.MealName,
                             Present = _attendance.Attendance,
                             Masked = false,
                             ChildId = _children.Id,
                             Day = _attendance.Date.Day
                         };
            return result;
        }
    }
}