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
                Attendance = g.Attendance,
                Date = new MealDate { Year = g.Date.Year, Month = g.Date.Month, Day = g.Date.Day },
                Name = s.Name
            });
            return result;
        }
    }
}