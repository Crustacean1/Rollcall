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
            .Include(c => c.MyGroup)
            .Where(child => child.Id == Id).FirstOrDefault();
        }

        public ICollection<Child> GetChildrenByGroup(int Id, bool track = false)
        {
            var query = track ? _context.Children : _context.Children.AsNoTracking();
            return query.Include(c => c.MyGroup)
            .Include(c => c.DefaultMeals)
            .Where(child => (child.GroupId == Id)).ToList();
        }
        public ICollection<Child> GetChildrenByGroup(bool track = false)
        {
            var query = track ? _context.Children : _context.Children.AsNoTracking();
            return query.Include(c => c.MyGroup)
            .Include(c => c.DefaultMeals)
            .ToList();
        }
        public void AddChildren(IEnumerable<Child> children)
        {
            _context.Children.AddRange(children);
        }
        public void RemoveChild(Child child)
        {
            _context.Children.Remove(child);
        }
    }
}