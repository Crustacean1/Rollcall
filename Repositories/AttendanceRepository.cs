using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
using System.Linq.Expressions;

namespace Rollcall.Repositories
{
    public class MealData
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Breakfast{get;set;}
        public int Dinner{get;set;}
        public int Desert{get;set;}
    }
    public class AttendanceRepository : RepositoryBase
    {
        public AttendanceRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<Attendance> GetChildAttendance(int childId, int year, int month, int day, bool track = false)
        {
            var query = track ? _context.Attendance : _context.Attendance.AsNoTracking();
            if (day == 0)
            {
                return query.Where(x => childId == x.ChildId && x.Date.Year == year && x.Date.Month == month);
            }
            return query.Where(x => childId == x.ChildId && x.Date.Year == year && x.Date.Month == month && (x.Date.Day == day || day == 0));
        }
        public IEnumerable<MealData>? GetGroupAttendance(int groupId, int year, int month)
        {
            var result =
            from attendance in _context.Set<Attendance>()
            from schema in _context.Set<MealSchema>()
            join allmasks in _context.Set<Mask>() on new { attendance.Date, attendance.TargetChild.GroupId } equals new { allmasks.Date, allmasks.GroupId } into nullMasks
            from masks in nullMasks.DefaultIfEmpty()
            where attendance.Date.Year == year && attendance.Date.Month == month && attendance.TargetChild.GroupId == groupId
            group new { schema, attendance, masks } by new { schema.Id, attendance.Date} into maskedAttendance
            select new MealData
            {
                MealName = maskedAttendance.First().schema.Name,
                Year = maskedAttendance.Key.Date.Year,
                Month = maskedAttendance.Key.Date.Month,
                Day = maskedAttendance.Key.Date.Day,
                Breakfast = maskedAttendance.Sum(m => m.attendance.Meals & (int)Math.Pow(2, m.schema.Id) & ((m.masks == null) ? 2047 : m.masks.Meals))
            };
            return result;
        }

        public void SetAttendance(Attendance attendance)
        {
            var prev = _context.Attendance.Where(a => a.Date == attendance.Date && a.ChildId == attendance.ChildId).FirstOrDefault();
            if (prev != null)
            {
                prev.Meals = attendance.Meals;
                return;
            }
            _context.Attendance.Add(attendance);
        }
    }
}