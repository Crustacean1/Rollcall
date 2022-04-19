using Rollcall.Models;

using System.Linq.Expressions;

namespace Rollcall.Specifications
{
    public class MonthlyAttendanceSpecification : ISummarySpecification<MonthlyMealGrouping, DailyMeal>
    {
        public Expression<Func<ChildMeal, bool>> Condition { get; }
        public IEnumerable<Expression<Func<ChildMeal, object>>> Includes { get; }
        public Expression<Func<ChildMeal, MonthlyMealGrouping>> Grouping { get; }
        public Expression<Func<IGrouping<MonthlyMealGrouping, ChildMeal>, DailyMeal>> Selection { get; }
        public bool Masked { get; }
        public MonthlyAttendanceSpecification(Child child, int year, int month)
        {
            Condition = (ChildMeal c) => c.ChildId == child.Id && c.Date.Year == year && c.Date.Month == month;
            Includes = new List<Expression<Func<ChildMeal, object>>>();
            Grouping = (ChildMeal c) => new MonthlyMealGrouping { Date = c.Date, MealName = c.MealName };
            Masked = false;
            Selection = (IGrouping<MonthlyMealGrouping, ChildMeal> res) => new DailyMeal
            {
                DayOfMonth = res.Key.Date.Day,
                MealName = res.Key.MealName,
                Total = res.Count()
            };
        }
        public MonthlyAttendanceSpecification(Group group, int year, int month)
        {
            Condition = (ChildMeal c) => c.TargetChild.GroupId == group.Id && c.Date.Year == year && c.Date.Month == month;
            Includes = new List<Expression<Func<ChildMeal, object>>>();
            Grouping = (ChildMeal c) => new MonthlyMealGrouping { Date = c.Date, MealName = c.MealName };
            Masked = false;
            Selection = (IGrouping<MonthlyMealGrouping, ChildMeal> res) => new DailyMeal
            {
                DayOfMonth = res.Key.Date.Day,
                MealName = res.Key.MealName,
                Total = res.Count()
            };
        }
        public MonthlyAttendanceSpecification(int year, int month)
        {
            Condition = (ChildMeal c) => c.Date.Year == year && c.Date.Month == month;
            Includes = new List<Expression<Func<ChildMeal, object>>>();
            Grouping = (ChildMeal c) => new MonthlyMealGrouping { Date = c.Date, MealName = c.MealName };
            Masked = true;
            Selection = (IGrouping<MonthlyMealGrouping, ChildMeal> res) => new DailyMeal
            {
                DayOfMonth = res.Key.Date.Day,
                MealName = res.Key.MealName,
                Total = res.Count()
            };
        }
    }
    public class MonthlyMealGrouping
    {
        public string MealName { get; set; }
        public DateTime Date { get; set; }
    }
    public class DailyMeal
    {
        public string MealName { get; set; }
        public int DayOfMonth { get; set; }
        public int Total { get; set; }
    }
}