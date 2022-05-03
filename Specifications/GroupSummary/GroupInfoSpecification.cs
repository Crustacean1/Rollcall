using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class GroupInfoSpecification : ISummarySpecification<InfoGrouping, MealInfo>
    {
        public Expression<Func<ChildMeal, bool>> Condition { get; }
        public IEnumerable<string> Includes { get; }
        public Expression<Func<ChildMeal, InfoGrouping>> Grouping { get; }
        public Expression<Func<IGrouping<InfoGrouping, ChildMeal>, MealInfo>> Selection { get; }
        public bool Masked { get; }
        public GroupInfoSpecification(Group group, DateTime date) : this()
        {
            Condition = (ChildMeal m) => m.TargetChild.GroupId == group.Id && m.Date == date;
        }
        public GroupInfoSpecification(DateTime date) : this()
        {
            Condition = (ChildMeal m) => m.Date == date;
        }
        public GroupInfoSpecification(Group group, int year, int month) : this()
        {
            Condition = (ChildMeal m) => m.TargetChild.GroupId == group.Id && m.Date.Year == year && m.Date.Month == month;
        }
        public GroupInfoSpecification(int year, int month) : this()
        {
            Condition = (ChildMeal m) => m.Date.Year == year && m.Date.Month == month;
        }
        private GroupInfoSpecification()
        {
            Includes = new List<string> { "TargetChild" , "TargetChild.MyGroup"};
            Grouping = (ChildMeal m) => new InfoGrouping
            {
                ChildId = m.ChildId,
                ChildName = m.TargetChild.Name,
                ChildSurname = m.TargetChild.Surname,
                GroupName = m.TargetChild.MyGroup.Name,
                MealName = m.MealName
            };
            Selection = (a) => new MealInfo
            {
                Name = a.Key.ChildName,
                Surname = a.Key.ChildSurname,
                ChildId = a.Key.ChildId,
                MealName = a.Key.MealName,
                GroupName = a.Key.GroupName,
                Total = a.Count()
            };
        }
    }
    public class InfoGrouping
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public string ChildSurname { get; set; }
        public string GroupName { get; set; }
        public string MealName { get; set; }
    }
}