using Microsoft.EntityFrameworkCore;

using Rollcall.Specifications;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class MaskRepository : BaseMealRepository<GroupMask>
    {
        public MaskRepository(RepositoryContext context) : base(context)
        {
        }
        public IEnumerable<GroupMask> GetTotalMask(ISpecification<GroupMask> spec)
        {
            var groupCount = _context.Set<Group>().Count();

            var maskQuery = _context.Set<GroupMask>().Where(spec.Condition).Where(m => m.Attendance);
            return maskQuery
            .GroupBy(m => new { m.Date, m.MealName })
            .Where(m => m.Count() == groupCount)
            .Select(t => new GroupMask
            {
                Date = t.Key.Date,
                MealName = t.Key.MealName,
                Attendance = true
            });
        }
    }
}