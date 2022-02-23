using Rollcall.Models;

namespace Rollcall.Services{
    public class SchemaService{
        static private readonly List<MealSchema> _schemas = new List<MealSchema>{
            new MealSchema{Id = 1, Name = "Breakfast"},
            new MealSchema{Id = 2, Name = "Dinner"},
            new MealSchema{Id = 3, Name = "Desert"},
        };
        public ICollection<MealSchema> GetSchemas(){
            return _schemas;
        }
    }
}