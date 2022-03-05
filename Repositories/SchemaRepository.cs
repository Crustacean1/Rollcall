using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class SchemaRepository : RepositoryBase
    {

        public SchemaRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<MealSchema> GetSchemas()
        {
            return _context.MealSchemas.AsNoTracking();
        }
    }
}