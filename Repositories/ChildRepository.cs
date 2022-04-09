using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
namespace Rollcall.Repositories
{
    public class ChildRepository : RepositoryBase
    {
        public ChildRepository(RepositoryContext context) : base(context) { }
        public Child? GetChild(int Id, bool track = false)
        {
            var query = track ? _context.Children : _context.Children.AsNoTracking();
            return query.Include(c => c.DefaultMeals)
            .ThenInclude(m => m.Schema)
            .Include(c => c.MyGroup)
            .Where(child => child.Id == Id).FirstOrDefault();
        }

        public ICollection<Child> GetChildrenByGroup(int Id, bool track = false)
        {
            var query = track ? _context.Children : _context.Children.AsNoTracking();
            return query.Include(c => c.MyGroup)
            .Include(c => c.DefaultMeals)
            .Where(child => (child.GroupId == Id || Id == 0)).ToList();
        }
        public ICollection<Child> GetChildrenByGroup(bool track = false)
        {
            var query = track ? _context.Children : _context.Children.AsNoTracking();
            return query.Include(c => c.MyGroup)
            .Include(c => c.DefaultMeals)
            .ThenInclude(d => d.Schema)
            .ToList();
        }
        public void AddChild(Child child)
        {
            _context.Children.Add(child);
        }

        public async Task AddDefaultMeal(Child child, DefaultAttendance attendance)
        {
            attendance.ChildId = child.Id;
            var oldAttendance = child.DefaultMeals.Where(a => a.MealId == attendance.MealId).FirstOrDefault();
            if (oldAttendance == null)
            {
                _context.Set<DefaultAttendance>().Add(attendance);
            }
            else
            {
                oldAttendance.Attendance = attendance.Attendance;
            }
            await SaveChangesAsync();
        }
        public void RemoveChild(Child child)
        {
            _context.Children.Remove(child);
        }
    }
}