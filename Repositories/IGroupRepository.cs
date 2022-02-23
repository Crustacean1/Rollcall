using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories{
    public interface IGroupRepository {
        public Group? GetGroup(int Id,bool track = false);
        public void AddGroup(Group group);
        public void RemoveGroup(Group group);
        public void SaveChanges();
        public Task SaveChangesAsync();
    }
}