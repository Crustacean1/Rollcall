using Rollcall.Models;

namespace Rollcall.Services
{
    public class SchemaService
    {
        static private readonly Dictionary<string, int> _schemas = new Dictionary<string, int>{
            {"breakfast",1},
            {"dinner",2},
            {"desert",3},
        };
        public Dictionary<string, int> GetSchemas()
        {
            return _schemas;
        }
    }
}