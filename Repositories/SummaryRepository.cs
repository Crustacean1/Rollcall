using Microsoft.EntityFrameworkCore;

using Rollcall.Models;
using Rollcall.Specifications;

namespace Rollcall.Repositories
{
    public class MealSummaryEntry
    {
        public int ChildId { get; set; }
        public string MealName { get; set; }
        public DateTime Date { get; set; }
    }
    public class MealSummary
    {
        public int ChildId { get; set; }
        public string MealName { get; set; }
        public DateTime Date { get; set; }
        public int Total { get; set; }
    }
    public class MealGrouping
    {
        public DateTime Date { get; set; }
        public string MealName { get; set; }
        public int ChildId { get; set; }
    }

    public class SummaryRepository
    {
        private readonly RepositoryContext _context;
        public SummaryRepository(RepositoryContext context)
        {
            _context = context;
        }

        public IEnumerable<MealSummary> GetGroupSummary<T>(ISummarySpecification<T> spec)
        {
            var query = _context.Set<ChildAttendance>()
            .AsNoTracking()
            .Where(spec.MealCondition);

            IQueryable<MealSummaryEntry> leftJoin = spec.Masked ? GetMaskedMeals(query) : GetUnmaskedMeals(query);

            return leftJoin.GroupBy(spec.Grouping).Select(spec.Selection);
        }
        private IQueryable<MealSummaryEntry> GetMaskedMeals(IQueryable<ChildAttendance> query)
        {
            return query.GroupJoin(_context.Set<GroupAttendance>().AsNoTracking(),
                 a => new { Date = a.Date, Name = a.MealName, TargetGroup = a.TargetChild.MyGroup },
                 a => new { Date = a.Date, Name = a.MealName, TargetGroup = a.TargetGroup },
                 (c, g) => new { ChildMeal = c, GroupMask = g })
                .SelectMany(q => q.GroupMask,
                (c, g) => new { Date = c.ChildMeal.Date, Name = c.ChildMeal.MealName, ChildId = c.ChildMeal.ChildId, Present = c.ChildMeal.Attendance, Masked = g.Attendance })
                .Where(a => a.Present && a.Masked != true)
                .Select(a => new MealSummaryEntry { MealName = a.Name, ChildId = a.ChildId, Date = a.Date });
        }
        private IQueryable<MealSummaryEntry> GetUnmaskedMeals(IQueryable<ChildAttendance> query)
        {
            return query
                .Where(a => a.Attendance)
                .Select(a => new MealSummaryEntry { MealName = a.MealName, ChildId = a.ChildId, Date = a.Date });
        }
    }
}