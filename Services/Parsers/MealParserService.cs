namespace Rollcall.Services
{
    public class MealParserService : IMealParserService
    {
        protected readonly ILogger<MealParserService> _logger;
        protected readonly Dictionary<string, int> _schemas;
        private uint AddMeal(uint meal, uint mealMask, bool value)
        {
            return (meal & (~mealMask) | (value ? mealMask : 0));
        }
        private uint SetMealAttendance(uint mealData, int mealId, bool attendance)
        {
            uint newMeal = (1u << mealId);
            mealData = AddMeal(mealData, newMeal, attendance);
            return mealData;
        }
        private uint GetMealAttendance(uint mealData, int mealId)
        {
            return (mealData >> mealId) & 1;
        }
        public MealParserService(SchemaService schemaService, ILogger<MealParserService> logger)
        {
            _logger = logger;
            _schemas = schemaService.GetSchemas();
        }
        public uint Parse(Dictionary<string, bool>? meals)
        {
            if (meals == null) { throw new InvalidDataException("In AttendanceParserService::MealsToInt(): parameters cannot be null"); }
            uint mealData = 0;
            foreach (var meal in meals)
            {
                if (!_schemas.ContainsKey(meal.Key))
                {
                    throw new InvalidDataException($"In AttendanceParserService::ConvertMealData(): Invalid meal property name: '{meal.Key}'");
                }
                mealData = SetMealAttendance(mealData, _schemas[meal.Key], meal.Value);
            }
            return mealData;
        }
        
        public Dictionary<string, bool> MarshallMask(uint mealData)
        {
            var meals = new Dictionary<string, bool>();
            foreach (var schema in _schemas)
            {
                meals.Add(schema.Key, GetMealAttendance(mealData, schema.Value) == 1u);
            }
            return meals;
        }
        public Dictionary<string, uint> MarshallMeal(uint mealData)
        {
            var meals = new Dictionary<string, uint>();
            foreach (var schema in _schemas)
            {
                meals.Add(schema.Key, GetMealAttendance(mealData, schema.Value));
            }
            return meals;
        }
        public uint ChangeAttendance(uint meal, Dictionary<string, bool> newMeals)
        {
            foreach (var newMeal in newMeals)
            {
                meal = SetMealAttendance(meal, _schemas[newMeal.Key], newMeal.Value);
            }
            return meal;
        }
        public uint GetFullAttendance()
        {
            return 7;
        }
        public uint GetEmptyAttendance()
        {
            return 0;
        }
    }
}