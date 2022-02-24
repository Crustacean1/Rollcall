using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories{
    public interface IGroupRepository {
        public IEnumerable<Group> GetGroups(bool track = false);
        public Group? GetGroup(int Id,bool track = false);
        public Group? GetGroup(string Name,bool track = false);
        public void AddGroup(Group group);
        public void RemoveGroup(Group group);
        public void SaveChanges();
        public Task SaveChangesAsync();
    }
}