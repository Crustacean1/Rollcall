using Microsoft.EntityFrameworkCore;

using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class MealRepository : BaseMealRepository<ChildMeal>
    {
        public MealRepository(RepositoryContext context) : base(context)
        { }
        public IEnumerable<DefaultMeal> GetMealsToExtend(int year, int month)
        {
            var children = _context.Set<Child>().AsNoTracking()
            .Include(c => c.DefaultMeals);

            var childrenWithNoMeals = children.Where(c => c.DailyMeals.Where(m => m.Date.Year == year && m.Date.Month == month).Count() == 0)
            .SelectMany(c => c.DefaultMeals,(c,m) => m);

            return childrenWithNoMeals;
        }
    }
}