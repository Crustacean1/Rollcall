using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class TotalSummarySpecification : ISummarySpecification<string, TotalSummaryResult>
    {
        public bool Masked { get; }
        public Expression<Func<ChildMeal, bool>> Condition { get; }
        public IEnumerable<Expression<Func<ChildMeal, object>>> Includes { get; }
        public Expression<Func<ChildMeal, string>> Grouping { get; }
        public Expression<Func<IGrouping<string, ChildMeal>, TotalSummaryResult>> Selection { get; }
        public TotalSummarySpecification(Child child, int year, int month, bool masked)
        {
            Masked = masked;
            Condition = (ChildMeal c) => c.ChildId == child.Id && c.Date.Year == year && c.Date.Month == month;
            Includes = new List<Expression<Func<ChildMeal, object>>>();
            Grouping = (ChildMeal c) => c.MealName;
            Selection = (IGrouping<string, ChildMeal> group) => new TotalSummaryResult { MealName = group.Key, Total = group.Count() };
        }
        public TotalSummarySpecification(Child child, int year, int month, int day, bool masked)
        {
            Masked = masked;
            Condition = (ChildMeal c) => c.ChildId == child.Id && c.Date == new DateTime(year, month, day);
            Includes = new List<Expression<Func<ChildMeal, object>>>();
            Grouping = (ChildMeal c) => c.MealName;
            Selection = (IGrouping<string, ChildMeal> group) => new TotalSummaryResult { MealName = group.Key, Total = group.Count() };
        }
        public TotalSummarySpecification(Group group, int year, int month, int day, bool masked)
        {
            Masked = masked;
            Condition = (ChildMeal c) => c.TargetChild.GroupId == group.Id && c.Date == new DateTime(year, month, day);
            Includes = new List<Expression<Func<ChildMeal, object>>>();
            Grouping = (ChildMeal c) => c.MealName;
            Selection = (IGrouping<string, ChildMeal> group) => new TotalSummaryResult { MealName = group.Key, Total = group.Count() };
        }
        public TotalSummarySpecification(Group group, int year, int month, bool masked)
        {
            Masked = masked;
            Condition = (ChildMeal c) => c.TargetChild.GroupId == group.Id && c.Date.Year == year && c.Date.Month == month;
            Includes = new List<Expression<Func<ChildMeal, object>>>();
            Grouping = (ChildMeal c) => c.MealName;
            Selection = (IGrouping<string, ChildMeal> group) => new TotalSummaryResult { MealName = group.Key, Total = group.Count() };
        }
        public TotalSummarySpecification(int year, int month, bool masked)
        {
            Masked = masked;
            Condition = (ChildMeal c) => c.Date.Year == year && c.Date.Month == month;
            Includes = new List<Expression<Func<ChildMeal, object>>>();
            Grouping = (ChildMeal c) => c.MealName;
            Selection = (IGrouping<string, ChildMeal> group) => new TotalSummaryResult { MealName = group.Key, Total = group.Count() };
        }
        public TotalSummarySpecification(int year, int month, int day, bool masked)
        {
            Masked = masked;
            Condition = (ChildMeal c) => c.Date == new DateTime(year, month, day);
            Includes = new List<Expression<Func<ChildMeal, object>>>();
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