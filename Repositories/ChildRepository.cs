using Rollcall.Specifications;
using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
namespace Rollcall.Repositories
{
    public class ChildRepository : RepositoryBase
    {
        public ChildRepository(RepositoryContext context) : base(context) { }
        public Child? GetChild(ISpecification<Child> spec)
        {
            var query = spec.Tracking ? _context.Children : _context.Children.AsNoTracking();
            var extendedQuery = spec.Includes.Aggregate(query, (q, s) => q.Include(s));
            var thinnedQuery = extendedQuery.Where(spec.Condition);
            return thinnedQuery.FirstOrDefault();
        }

        public IEnumerable<Child> GetChildrenByGroup(ISpecification<Child> spec)
        {
            var query = spec.Tracking ? _context.Children : _context.Children.AsNoTracking();
            var extendedQuery = spec.Includes.Aggregate(query, (q, s) => q.Include(s));
            var thinnedQuery = extendedQuery.Where(spec.Condition);
            return thinnedQuery;
        }
        public void AddChild(Child child)
        {
            _context.Children.Add(child);
        }

        public void RemoveChild(Child child)
        {
            _context.Children.Remove(child);
        }
    }
}