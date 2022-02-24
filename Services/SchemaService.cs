using Rollcall.Models;

namespace Rollcall.Services
{
    public class SchemaService
    {
        static private readonly Dictionary<string, int> _schemas = new Dictionary<string, int>{
            {"breakfast",0},
            {"dinner",1},
            {"desert",2},
        };
        public Dictionary<string, int> GetSchemas()
        {
            return _schemas;
        }
    }
}