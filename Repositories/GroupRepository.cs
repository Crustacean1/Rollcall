using Microsoft.EntityFrameworkCore;

using Rollcall.Specifications;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class GroupRepository : RepositoryBase
    {
        public GroupRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<Group> GetGroups(bool track = false)
        {
            var query = track ? _context.Groups : _context.Groups.AsNoTracking();
            return query;
        }
        public Group? GetGroup(ISpecification<Group> spec)
        {
            var query = spec.Tracking ? _context.Groups : _context.Groups.AsNoTracking();
            return IncludeOthers(query, spec.Includes)
            .Where(spec.Condition)
            .FirstOrDefault();
        }
        public void AddGroup(Group group)
        {
            _context.Add(group);
        }
    }
}