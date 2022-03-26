
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
        public int Day { get; set; }
        public bool Masked { get; set; }
        public bool Present { get; set; }
        public string MealName { get; set; }
        public int ChildId { get; set; }
    }

    public class AttendanceRepositoryBase : RepositoryBase
    {
        public AttendanceRepositoryBase(RepositoryContext context) : base(context) { }
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