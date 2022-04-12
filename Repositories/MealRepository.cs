using Rollcall.Models;
using Rollcall.Specifications;

namespace Rollcall.Repositories
{
    public class MealRepository
    {
        private readonly DbContext _context;
        public MealRepository(RepositoryContext context)
        {
            _context = context;
        }

        public ChildMeal GetCount(ISpecification<Child> spec)
        {
            var query = _context.Set<Child>()
            .AsNoTracking();
            var expandedQuery = spec.Includes.Aggegate(query, (q, i) => q.Include(i));
            var groupedQuery = spec.Groups.Aggregate(expandedQuery, (q, g) => q.GroupBy(g));
            return expandedQuery.Where(spec.Condition);
        }
    }
}