using Microsoft.EntityFrameworkCore;

using Rollcall.Specifications;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class MaskRepository : BaseMealRepository<GroupMask>
    {
        public MaskRepository(RepositoryContext context) : base(context)
        { }
        public IEnumerable<GroupMask> GetTotalMask(ISpecification<GroupMask> spec)
        {
            var groupQuery = _context.Set<Group>()
            .AsNoTracking();
            var maskQuery = _context.Set<GroupMask>().Where(spec.Condition);
            return groupQuery.GroupJoin(maskQuery, g => g.Id, m => m.GroupId, (g, m) => new { g, m })
            .SelectMany(j => j.m.DefaultIfEmpty(), (l, m) => new { Present = m.Attendance, Date = m.Date, GroupId = l.g.Id , MealName = m.MealName})
            .GroupBy(m => new { m.Date, m.MealName })
            .Select(t => new GroupMask
            {
                Attendance = t.All(m => m.Present == true),
                Date = t.Key.Date,
                MealName = t.Key.MealName
            });
        }
    }
}