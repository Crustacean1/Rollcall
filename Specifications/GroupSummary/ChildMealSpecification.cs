using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class ChildMealSpecification : ISpecification<ChildMeal>
    {
        public Expression<Func<ChildMeal, bool>> Condition { get; }
        public IEnumerable<Expression<Func<ChildMeal, object>>> Includes { get; }
        public bool Tracking { get; }
        public ChildMealSpecification(Child child, int year, int month)
        {
            Condition = (ChildMeal a) => a.ChildId == child.Id && a.Date.Year == year && a.Date.Month == month;
            Includes = new List<Expression<Func<ChildMeal, object>>>();
            Tracking = false;
        }
        public ChildMealSpecification(Child child, int year, int month, int day)
        {
            Condition = (ChildMeal a) => a.ChildId == child.GroupId && a.Date == new DateTime(year, month, day);
            Includes = new List<Expression<Func<ChildMeal, object>>>();
            Tracking = false;
        }
    }
}