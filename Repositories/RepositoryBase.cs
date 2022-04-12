using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Rollcall.Repositories
{
    public class RepositoryBase
    {
        protected readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        protected IQueryable<T> GetSetWhere<T>(Expression<Func<T, bool>> condition) where T : class
        {
            return _context.Set<T>()
            .AsNoTracking()
            .Where(condition);
        }
        protected IQueryable<T> IncludeOthers<T>(IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> includes) where T : class
        {
            return includes.Aggregate(query, (query, include) => query.Include(include));
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}