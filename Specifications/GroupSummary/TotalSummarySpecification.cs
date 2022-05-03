using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class TotalSummarySpecification : ISummarySpecification<string, TotalSummaryResult>
    {
        public bool Masked { get; }
        public Expression<Func<ChildMeal, bool>> Condition { get; }
        public IEnumerable<string> Includes { get; }
        public Expression<Func<ChildMeal, string>> Grouping { get; }
        public Expression<Func<IGrouping<string, ChildMeal>, TotalSummaryResult>> Selection { get; }
        public TotalSummarySpecification(Child child, int year, int month, bool masked) : this(masked)
        {
            Condition = (ChildMeal c) => c.ChildId == child.Id && c.Date.Year == year && c.Date.Month == month;
        }
        public TotalSummarySpecification(Child child, int year, int month, int day, bool masked) : this(masked)
        {
            Condition = (ChildMeal c) => c.ChildId == child.Id && c.Date == new DateTime(year, month, day);
        }
        public TotalSummarySpecification(Group group, int year, int month, int day, bool masked) : this(masked)
        {
            Condition = (ChildMeal c) => c.TargetChild.GroupId == group.Id && c.Date == new DateTime(year, month, day);
        }
        public TotalSummarySpecification(Group group, int year, int month, bool masked) : this(masked)
        {
            Condition = (ChildMeal c) => c.TargetChild.GroupId == group.Id && c.Date.Year == year && c.Date.Month == month;
        }
        public TotalSummarySpecification(int year, int month, bool masked) : this(masked)
        {
            Condition = (ChildMeal c) => c.Date.Year == year && c.Date.Month == month;
        }
        public TotalSummarySpecification(int year, int month, int day, bool masked) : this(masked)
        {
            Condition = (ChildMeal c) => c.Date == new DateTime(year, month, day);
        }
        private TotalSummarySpecification(bool masked)
        {
            Masked = masked;
            Includes = new List<string>();
            Grouping = (ChildMeal c) => c.MealName;
            Selection = (IGrouping<string, ChildMeal> group) => new TotalSummaryResult { MealName = group.Key, Total = group.Count() };
        }
    }
    public class TotalSummaryResult
    {
        public string MealName { get; set; }
        public int Total { get; set; }
    }
}