using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class GroupSummarySpecification : IGroupingSpecification<Child>
    {
        public IEnumerable<Expression<Func<Child, object>>> Includes { get; }
        public Expression<Func<Child, bool>> Condition { get; }
        public Expression<Func<Child, object>> Grouping { get; }
        public bool Tracking { get; }
        GroupSummarySpecification(int year,int month,int day)
        {
            Tracking = false;
            Includes = new List<Expression<Func<Child,object>>>{
                (Child attendance) => attendance.DailyMeals,
                (Child attendance) => attendance.MyGroup.,
            };
        }
        GroupSummarySpecification(int year,int month,int day)
        {
            Tracking = false;
            Includes = new List<Expression<Func<ChildAttendance,object>>>{
                (ChildAttendance attendance) => attendance.
            };
        }

    }
}