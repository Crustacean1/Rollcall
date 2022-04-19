using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class GroupMealSpecification : ISpecification<GroupMask>
    {
        public Expression<Func<GroupMask, bool>> Condition { get; }
        public IEnumerable<Expression<Func<GroupMask, object>>> Includes { get; }
        public bool Tracking { get; }
        public GroupMealSpecification(Child child, int year, int month)
        {
            Condition = (GroupMask a) => a.GroupId == child.GroupId && a.Date.Year == year && a.Date.Month == month;
            Includes = new List<Expression<Func<GroupMask, object>>>();
            Tracking = false;
        }
        public GroupMealSpecification(Child child, int year, int month, int day)
        {
            Condition = (GroupMask a) => a.GroupId == child.GroupId && a.Date == new DateTime(year, month, day);
            Includes = new List<Expression<Func<GroupMask, object>>>();
            Tracking = false;
        }
        public GroupMealSpecification(Group group, int year, int month)
        {
            Condition = (GroupMask a) => a.GroupId == group.Id && a.Date.Year == year && a.Date.Month == month;
            Includes = new List<Expression<Func<GroupMask, object>>>();
            Tracking = false;
        }
        public GroupMealSpecification(Group group, int year, int month, int day)
        {
            Condition = (GroupMask a) => a.GroupId == group.Id && a.Date == new DateTime(year, month, day);
            Includes = new List<Expression<Func<GroupMask, object>>>();
            Tracking = false;
        }
        public GroupMealSpecification(int year, int month)
        {
            Condition = (GroupMask a) => a.Date.Year == year && a.Date.Month == month;
            Includes = new List<Expression<Func<GroupMask, object>>>();
            Tracking = false;
        }
        public GroupMealSpecification(int year, int month, int day)
        {
            Condition = (GroupMask a) => a.Date == new DateTime(year, month, day);
            Includes = new List<Expression<Func<GroupMask, object>>>();
            Tracking = false;
        }
    }
}