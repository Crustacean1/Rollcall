using Microsoft.EntityFrameworkCore;
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
        public Group? GetGroup(string name, bool track = false)
        {
            var query = track ? _context.Groups : _context.Groups.AsNoTracking();
            return query.Where(group => group.Name == name).FirstOrDefault();
        }
        public Group? GetGroup(int Id, bool track = false)
        {
            var query = track ? _context.Groups : _context.Groups.AsNoTracking();
            return query.Where(group => group.Id == Id).FirstOrDefault();
        }
        public void AddGroup(Group group)
        {
            _context.Add(group);
        }
        public void RemoveGroup(Group group)
        {
            _context.Remove(group);
        }
    }
}