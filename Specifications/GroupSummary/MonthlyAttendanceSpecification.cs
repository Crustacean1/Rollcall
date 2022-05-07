using Rollcall.Models;

using System.Linq.Expressions;

namespace Rollcall.Specifications
{
    public class MonthlyAttendanceSpecification : IMealSpecification<MonthlyMealGrouping, DailyMeal>
    {
        public Expression<Func<ChildMeal, bool>> Condition { get; }
        public IEnumerable<string> Includes { get; }
        public Expression<Func<ChildMeal, MonthlyMealGrouping>> Grouping { get; }
        public Expression<Func<IGrouping<MonthlyMealGrouping, ChildMeal>, DailyMeal>> Selection { get; }
        public bool Masked { get; }
        public MonthlyAttendanceSpecification(Child child, int year, int month) : this(false)
        {
            Condition = (ChildMeal c) => c.ChildId == child.Id && c.Date.Year == year && c.Date.Month == month;
            Grouping = (ChildMeal c) => new MonthlyMealGrouping { Date = c.Date, MealName = c.MealName };
            Selection = (IGrouping<MonthlyMealGrouping, ChildMeal> res) => new DailyMeal
            {
                DayOfMonth = res.Key.Date.Day,
                MealName = res.Key.MealName,
                Total = res.Count()
            };
        }
        public MonthlyAttendanceSpecification(Group group, int year, int month) : this(false)
        {
            Condition = (ChildMeal c) => c.TargetChild.GroupId == group.Id && c.Date.Year == year && c.Date.Month == month;
            Grouping = (ChildMeal c) => new MonthlyMealGrouping { Date = c.Date, MealName = c.MealName };
            Selection = (IGrouping<MonthlyMealGrouping, ChildMeal> res) => new DailyMeal
            {
                DayOfMonth = res.Key.Date.Day,
                MealName = res.Key.MealName,
                Total = res.Count()
            };
        }
        public MonthlyAttendanceSpecification(int year, int month) : this(true)
        {
            Condition = (ChildMeal c) => c.Date.Year == year && c.Date.Month == month;
            Grouping = (ChildMeal c) => new MonthlyMealGrouping { Date = c.Date, MealName = c.MealName };
            Selection = (IGrouping<MonthlyMealGrouping, ChildMeal> res) => new DailyMeal
            {
                DayOfMonth = res.Key.Date.Day,
                MealName = res.Key.MealName,
                Total = res.Count()
            };
        }
        private MonthlyAttendanceSpecification(bool masked)
        {
            Masked = masked;
            Includes = new List<string>();
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