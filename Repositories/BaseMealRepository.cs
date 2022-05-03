using Microsoft.EntityFrameworkCore;

using Rollcall.Specifications;

namespace Rollcall.Repositories
{
    public class BaseMealRepository<MealType> : RepositoryBase where MealType : class
    {
        public BaseMealRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<MealType> GetMeals(ISpecification<MealType> spec)
        {
            var query = spec.Tracking ? _context.Set<MealType>() : _context.Set<MealType>().AsNoTracking();
            return query.Where(spec.Condition);
        }
        public void UpdateMeals(IEnumerable<MealType> masks)
        {
            foreach (var mask in masks)
            {
                _context.Entry(mask).State = EntityState.Modified;
            }
        }
        public void CreateMeals(IEnumerable<MealType> masks)
        {
            foreach (var mask in masks)
            {
                _context.Entry(mask).State = EntityState.Added;
            }
        }
    }
}