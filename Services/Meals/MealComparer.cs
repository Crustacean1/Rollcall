using Rollcall.Models;

namespace Rollcall.Services
{
    class MealComparer<MealType> : IEqualityComparer<MealType> where MealType : IMeal
    {
        public bool Equals(MealType? a, MealType? b)
        {
            return a is not null && b is not null && a.MealName == b.MealName;
        }
        public int GetHashCode(MealType a)
        {
            return a.MealName.GetHashCode() ^ a.Date.GetHashCode();
        }
    }
}