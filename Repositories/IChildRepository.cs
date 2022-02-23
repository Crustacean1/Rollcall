using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
namespace Rollcall.Repositories{
    public interface IChildRepository {
        public Child? GetChild(int Id,bool track = false);

        public ICollection<Child> GetChildrenByGroup(int Id,bool track = false);
        public ICollection<Child> GetChildrenByGroup(bool track = false);
        public void AddChildren(IEnumerable<Child> children);
        public void RemoveChild(Child child);
        public void SaveChanges();
        public Task SaveChangesAsync();
    }
}