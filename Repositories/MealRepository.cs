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
        public void AddChildMeal(ChildAttendance childMeal)
        {
            _context.Set<ChildAttendance>().Add(childMeal);
        }
    }
}