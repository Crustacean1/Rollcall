using Microsoft.EntityFrameworkCore;

using Rollcall.Models;
namespace Rollcall.Repositories
{
    public class MealRepository : RepositoryBase
    {
        public MealRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<ChildAttendance> GetChildAttendance(int childId, DateTime date)
        {
            return GetSetWhere<ChildAttendance>(c => c.ChildId == childId && c.Date == date);
        }
        public void UpdateChildAttendance(IEnumerable<ChildAttendance> updates)
        {
            foreach (var meal in updates)
            {
                _context.Entry(meal).State = EntityState.Modified;
            }
        }
        public void AddChildAttendance(IEnumerable<ChildAttendance> newAttendances)
        {
            foreach (var meal in newAttendances)
            {
                _context.Entry(meal).State = EntityState.Added;
            }
        }
    }
}