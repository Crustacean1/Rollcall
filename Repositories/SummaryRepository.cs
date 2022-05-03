using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Rollcall.Models;
using Rollcall.Specifications;

namespace Rollcall.Repositories
{
    public class SummaryRepository
    {
        private readonly RepositoryContext _context;
        public SummaryRepository(RepositoryContext context)
        {
            _context = context;
        }
        public IEnumerable<ResultType> GetMeals<GroupingType, ResultType>(ISummarySpecification<GroupingType, ResultType> spec)
         where GroupingType : class
         where ResultType : class
        {
            var query = _context.Set<ChildMeal>()
            .AsNoTracking()
            .Where(m => m.Attendance);

            var leftJoin = (spec.Masked ? GetMaskedMeals(query) : query)
            .Where(spec.Condition);

            var extendedQuery = spec.Includes.Aggregate(leftJoin, (query, include) => query.Include(include));

            return leftJoin.GroupBy(spec.Grouping).Select(spec.Selection);
        }
        public IEnumerable<MealInfo> GetMealInfo<GroupingType>(ISummarySpecification<GroupingType, MealInfo> spec) where GroupingType : class
        {
            var query = _context.Set<ChildMeal>()
            .AsNoTracking()
            .Where(spec.Condition);
            var extendedQuery = spec.Includes.Aggregate(query, (q, i) => q.Include(i));
            return extendedQuery.GroupBy(spec.Grouping).Select(spec.Selection);
        }
        private IQueryable<ChildMeal> GetMaskedMeals(IQueryable<ChildMeal> query)
        {
            var maskQuery = _context
            .Set<GroupMask>()
            .Where(m => m.Attendance);

            return query.GroupJoin(maskQuery,
                 a => new { Date = a.Date, Name = a.MealName, GroupId = a.TargetChild.GroupId },
                 a => new { Date = a.Date, Name = a.MealName, GroupId = a.GroupId },
                 (c, g) => new { ChildMeal = c, Mask = g })
                .SelectMany(q => q.Mask.DefaultIfEmpty(),
                (c, g) => new { Meal = c.ChildMeal, Masked = g })
                .Where(a => a.Masked.Attendance != true)
                .Select(a => a.Meal);
        }
    }
}