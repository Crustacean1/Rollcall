using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
namespace Rollcall.Repositories
{
    public class ChildRepository : RepositoryBase, IChildRepository
    {
        public ChildRepository(RepositoryContext context) : base(context) { }
        public Child? GetChild(int Id, bool track = false)
        {
            var query = track ? _context.Children : _context.Children.AsNoTracking();
            return query.Where(child => child.Id == Id).FirstOrDefault();
        }

        public ICollection<Child> GetChildrenByGroup(int Id, bool track = false)
        {
            var query = track ? _context.Children : _context.Children.AsNoTracking();
            return query.Where(child => (child.GroupId == Id || Id == 0)).ToList();
        }
        public ICollection<Child> GetChildrenByGroup(bool track = false)
        {
            var query = track ? _context.Children : _context.Children.AsNoTracking();
            return query.ToList();
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