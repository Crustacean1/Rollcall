using Rollcall.Models;
using Microsoft.EntityFrameworkCore;

namespace Rollcall.Repositories{
    public class UserRepository{
        private readonly RepositoryContext _repoContext;
        public UserRepository(RepositoryContext context){
            _repoContext = context;
        }
        public User? GetUser(string login){
            return _repoContext.Users.Where((user) => (user.Login == login)).FirstOrDefault();
        }
        public void AddUser(User user){
            _repoContext.Users.Add(user);
        }
        public async Task SaveChangesAsync(){
            await _repoContext.SaveChangesAsync();
        }
        public void SaveChanges(){
            _repoContext.SaveChanges();
        }

    }
}