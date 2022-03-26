using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class ChildAttendanceEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ChildId { get; set; }
        public string MealName { get; set; }
        public int Present { get; set; }
    }
    public class AttendanceSummaryRepository : AttendanceRepositoryBase
    {
        public AttendanceSummaryRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<ChildAttendanceEntity> GetMonthlySummary(int year, int month)
        {
            var children = _context.Set<Child>();
            var childAttendance = GetSetWhere<ChildAttendance>(ChildDateCondition(year,month,0))
            .Where(a => a.Attendance);
            var groupAttendance = GetSetWhere<GroupAttendance>(GroupDateCondition(year,month,0))
            .Where(a => a.Attendance);
            var totalAttendance = JoinAttendances(children, childAttendance, groupAttendance);
            return totalAttendance;
        }
        public IEnumerable<ChildAttendanceEntity> GetDailyList(MealDate date)
        {
            var children = _context.Set<Child>();
            var childAttendance = GetSetWhere(ChildDateCondition(date.Year,date.Month,date.Day))
            .Where(a => a.Attendance);
            var groupAttendance = GetSetWhere(GroupDateCondition(date.Year,date.Month,date.Day))
            .Where(a => a.Attendance);
            var totalAttendance = JoinAttendances(children, childAttendance, groupAttendance);
            return totalAttendance;
        }
        private IEnumerable<ChildAttendanceEntity> JoinAttendances(IQueryable<Child> children, IQueryable<ChildAttendance> childAttendance, IQueryable<GroupAttendance> groupAttendance)
        {
            var result = from _children in children
                         from  _mealSchema in _context.Set<MealSchema>().AsNoTracking()
                         join _ca in childAttendance on new {cId = _children.Id,mId = _mealSchema.Id} equals new{cId = _ca.ChildId,mId = _ca.MealId} into _sparseCa
                         from _childAttendance in _sparseCa.DefaultIfEmpty()
                         join _ga in groupAttendance on new { _children.GroupId, _childAttendance.Date, _childAttendance.MealId } equals new { _ga.GroupId, _ga.Date, _ga.MealId } into _sparseGa
                         from _groupAttendance in _sparseGa.DefaultIfEmpty()
                         group new ChildAttendanceEntry
                         {
                             Name = _children.Name,
                             Surname = _children.Surname,
                             ChildId = _children.Id,
                             MealName = _mealSchema.Name,
                             Masked = _groupAttendance == null ? false : _groupAttendance.Attendance,
                             Present = _childAttendance == null ? false: _childAttendance.Attendance
                         } by new
                         { ChildId = _children.Id,Surname = _children.Surname, ChildName = _children.Name, MealName = _mealSchema.Name } into attendance
                         select new ChildAttendanceEntity
                         {
                             Name = attendance.Key.ChildName,
                             Surname = attendance.Key.Surname,
                             ChildId = attendance.Key.ChildId,
                             MealName = attendance.Key.MealName,
                             Present = attendance.Count(a => a.Present&&!a.Masked)
                         };
            return result;
        }
    }
    class ChildAttendanceEntry
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MealName { get; set; }
        public int ChildId { get; set; }
        public bool Masked { get; set; }
        public bool Present{get;set;}
    }
}