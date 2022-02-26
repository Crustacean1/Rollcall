namespace Rollcall.Services
{
    public class MealParserService : IMealParserService
    {
        protected readonly Dictionary<string, int> _schemas;
        private void SetMealAttendance(ref uint mealData, bool attendance, int mealId)
        {
            uint mask = ((attendance ? 1u : 0u) << mealId);
            mealData = (mealData & (~mask)) | mask;
        }
        private uint GetMealAttendance(uint mealData, int mealId)
        {
            return (mealData >> mealId) & 1;
        }
        public MealParserService(SchemaService schemaService)
        {
            _schemas = schemaService.GetSchemas();
        }
        public uint FromDict(Dictionary<string, bool>? meals)
        {
            if (meals == null) { throw new InvalidDataException("In AttendanceParserService::MealsToInt(): parameters cannot be null"); }
            uint mealData = 0;
            foreach (var meal in meals)
            {
                if (!_schemas.ContainsKey(meal.Key))
                {
                    throw new InvalidDataException($"In AttendanceParserService::ConvertMealData(): Invalid meal property name: '{meal.Key}'");
                }
                SetMealAttendance(ref mealData, meal.Value, _schemas[meal.Key]);
            }
            return mealData;
        }
        public Dictionary<string, bool> ToDict(uint mealData)
        {
            var meals = new Dictionary<string, bool>();
            foreach (var schema in _schemas)
            {
                meals.Add(schema.Key, GetMealAttendance(mealData, schema.Value) == 1u);
            }
            return meals;
        }

    }
}