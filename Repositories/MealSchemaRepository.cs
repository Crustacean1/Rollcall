using Rollcall.Models;
namespace Rollcall.Repositories
{
    public class MealSchemaRepository : RepositoryBase
    {
        public MealSchemaRepository(RepositoryContext context) : base(context) { }

        public IEnumerable<MealSchema> ParseMeals(IEnumerable<string> mealNames)
        {
            return GetSetWhere<MealSchema>(s => mealNames.Contains(s.Name));
        }
        public int CheckIfMealsExist(IEnumerable<int> mealIds)
        {
            return GetSetWhere<MealSchema>(s => mealIds.Contains(s.Id)).Count() - mealIds.Count();
        }
    }
}