using Rollcall.Models;

namespace Rollcall.Services
{
    public class SchemaService
    {
        private readonly Dictionary<string, int> _mealIdTranslation;
        private readonly Dictionary<int, string> _idMealTranslation;
        public SchemaService(IEnumerable<MealSchema> data)
        {
            _mealIdTranslation = data.ToDictionary(m => m.Name, m => m.Id);
            _idMealTranslation = data.ToDictionary(m => m.Id, m => m.Name);
        }
        public string Translate(int id)
        {
            return _idMealTranslation[id];
        }
        public int Translate(string name)
        {
            return _mealIdTranslation[name];
        }
        public IEnumerable<string> GetNames(){
            return _mealIdTranslation.Keys;
        }
    }
}