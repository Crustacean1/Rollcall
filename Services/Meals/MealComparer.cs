using Rollcall.Models;

namespace Rollcall.Services
{
    class ChildMealComparer : IEqualityComparer<ChildMeal>
    {
        public bool Equals(ChildMeal? a, ChildMeal? b)
        {
            return a is not null && b is not null && a.MealName == b.MealName && a.ChildId == b.ChildId;
        }
        public int GetHashCode(ChildMeal a)
        {
            return a.MealName.GetHashCode() ^ a.Date.GetHashCode();
        }
    }
    class GroupMealComparer : IEqualityComparer<GroupMask>
    {
        public bool Equals(GroupMask? a, GroupMask? b)
        {
            return a is not null && b is not null && a.MealName == b.MealName && a.GroupId == b.GroupId;
        }
        public int GetHashCode(GroupMask a)
        {
            return a.MealName.GetHashCode() ^ a.Date.GetHashCode();
        }
    }
}