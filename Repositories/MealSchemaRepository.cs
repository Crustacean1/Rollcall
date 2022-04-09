using Rollcall.Models;
namespace Rollcall.Repositories
{
    public class MealSchemaRepository : RepositoryBase
    {
        public MealSchemaRepository(RepositoryContext context) : base(context) { }

        public int CountValidMeals(IEnumerable<string> mealIds)
        {
            return GetSetWhere<MealSchema>(s => mealIds.Contains(s.Name)).Count();
        }
    }
}