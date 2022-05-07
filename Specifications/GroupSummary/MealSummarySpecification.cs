using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class MealSummarySpecification : ISummarySpecification<InfoGrouping, MealInfo>
    {
        public Expression<Func<ChildMeal, bool>> Condition { get; }
        public Expression<Func<Child, bool>> SummaryCondition { get; }
        public IEnumerable<string> Includes { get; }
        public Expression<Func<ChildMeal, InfoGrouping>> Grouping { get; }
        public Expression<Func<IGrouping<InfoGrouping, ChildMeal>, MealInfo>> Selection { get; }
        public bool Masked { get; }
        public MealSummarySpecification(Group group, DateTime date, bool masked = false) : this(masked)
        {
            Condition = (ChildMeal m) => m.Date == date;
            SummaryCondition = (Child c) => c.GroupId == group.Id;
        }
        public MealSummarySpecification(DateTime date, bool masked = false) : this(masked)
        {
            Condition = (ChildMeal m) => m.Date == date;
            SummaryCondition = (Child c) => true;
        }
        public MealSummarySpecification(Group group, int year, int month, bool masked = false) : this(masked)
        {
            Condition = (ChildMeal m) => m.Date.Year == year && m.Date.Month == month;
            SummaryCondition = (Child c) => c.GroupId == group.Id;
        }
        public MealSummarySpecification(int year, int month, bool masked = false) : this(masked)
        {
            Condition = (ChildMeal m) => m.Date.Year == year && m.Date.Month == month;
            SummaryCondition = (Child c) => true;
        }
        private MealSummarySpecification(bool masked)
        {
            Includes = new List<string> { };
            Grouping = (ChildMeal m) => new InfoGrouping
            {
                ChildId = m.ChildId,
                MealName = m.MealName
            };
            Selection = (a) => new MealInfo
            {
                ChildId = a.Key.ChildId,
                MealName = a.Key.MealName,
                Total = a.Count()
            };
            Masked = masked;
        }
    }
    public class InfoGrouping
    {
        public int ChildId { get; set; }
        public string MealName { get; set; }
    }
}