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
        public IEnumerable<ResultType> GetMeals<GroupingType, ResultType>(IMealSpecification<GroupingType, ResultType> spec)
         where GroupingType : class
         where ResultType : class
        {
            return SelectMeals(spec);
        }
        public IEnumerable<MealInfo> GetMealSummary<GroupingType>(ISummarySpecification<GroupingType, MealInfo> spec) where GroupingType : class
        {
            var mealQuery = SelectMeals(spec);
            var childrenWithMeals = _context.Set<Child>()
            .Where(spec.SummaryCondition)
            .Join(_context.Set<MealSchema>(), c => true, s => true, (child, schema) => new MealContext { Child = child, MealName = schema.Name });

            return childrenWithMeals
            .GroupJoin(mealQuery,
            c => new { Id = c.Child.Id, MealName = c.MealName },
            m => new { Id = m.ChildId, MealName = m.MealName }, (context, meal) => new { context, meal })
            .SelectMany(g => g.meal.DefaultIfEmpty(), (g, m) => new MealInfo
            {
                Name = g.context.Child.Name,
                Surname = g.context.Child.Surname,
                ChildId = g.context.Child.Id,
                GroupName = g.context.Child.MyGroup.Name,
                GroupId = g.context.Child.GroupId,
                MealName = g.context.MealName,
                Total = (m.Total == null) ? 0 : m.Total
            });
        }
        private IQueryable<ResultType> SelectMeals<GroupingType, ResultType>(IMealSpecification<GroupingType, ResultType> spec)
         where GroupingType : class
         where ResultType : class
        {
            var query = _context.Set<ChildMeal>()
            .AsNoTracking()
            .Where(m => m.Attendance);

            var leftJoin = (spec.Masked ? GetMaskedMeals(query) : query)
            .Where(spec.Condition);

            return leftJoin.GroupBy(spec.Grouping).Select(spec.Selection);
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
    public class MealContext
    {
        public Child Child { get; set; }
        public string MealName { get; set; }
    }
}