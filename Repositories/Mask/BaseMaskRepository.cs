using System.Linq.Expressions;
using Rollcall.Models;
using Microsoft.EntityFrameworkCore;

namespace Rollcall.Repositories
{
    public class MaskRepositoryBase
    {
        private readonly RepositoryContext _context;
        public MaskRepositoryBase(RepositoryContext context)
        {
            _context = context;
        }
        protected IEnumerable<MaskEntity> ConstructQuery(Expression<Func<GroupAttendance, bool>> targetCondition,
        Expression<Func<GroupAttendance, bool>> dateCondition)
        {
            var result = _context.Set<GroupAttendance>().AsNoTracking()
            .Where(targetCondition)
            .Where(dateCondition)
            .Join(_context.Set<MealSchema>().AsNoTracking(), g => g.MealId, s => s.Id, (g, s) => new MaskEntity
            {
                Masked = g.Attendance,
                Date = new MealDate { Year = g.Date.Year, Month = g.Date.Month, Day = g.Date.Day },
                Name = s.Name
            });
            return result;
        }
        protected IEnumerable<MaskEntity> GeneralQuery(Expression<Func<GroupAttendance, bool>> dateCondition)
        {
            var groupCount = _context.Set<Group>().Count();

            var masks = _context.Set<GroupAttendance>()
            .AsNoTracking()
            .Where(dateCondition)
            .Where(g => g.Attendance);

            var result = masks
            .Join(_context.Set<MealSchema>().AsNoTracking(), a => a.MealId, s => s.Id, (a, s) => new MaskEntity
            {
                Masked = a.Attendance,
                Name = s.Name,
                Date = new MealDate(a.Date.Year, a.Date.Month, a.Date.Day)
            })
            .GroupBy(m => new { m.Date, m.Name })
            .Select(m => new MaskEntity
            {
                Masked = m.Count() == groupCount,
                Name = m.Key.Name,
                Date = m.Key.Date
            });
            return result;
        }
        protected Expression<Func<GroupAttendance, bool>> DateCondition(int year, int month, int day)
        {
            if (day == 0)
            {
                return (a) => a.Date.Year == year & a.Date.Month == month;
            }
            return a => a.Date == new DateTime(year, month, day);
        }
    }
}