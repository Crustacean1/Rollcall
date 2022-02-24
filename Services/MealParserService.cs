using Rollcall.Models;
namespace Rollcall.Services
{
    public class MealParserService : IMealParserService
    {
        private void SetMealAttendance(ref int mealData, bool attendance, int mealId)
        {
            var mask = ((attendance ? 1 : 0) << mealId);
            mealData = (mealData & (~mask)) | mask;
        }
        private bool GetMealAttendance(int mealData, int mealId)
        {
            return ((mealData >> mealId) & 1) == 1;
        }

        public int FromDto(Dictionary<string, bool>? meals, Dictionary<string, int>? schemas)
        {
            if (schemas == null || meals == null) { throw new InvalidDataException("In AttendanceHandlerService::MealsToInt(): parameters cannot be null"); }
            int mealData = 0;
            foreach (var meal in meals)
            {
                if (!schemas.ContainsKey(meal.Key))
                {
                    throw new InvalidDataException($"In AttendanceHandlerService::ConvertMealData(): Invalid meal property name: '{meal.Key}'");
                }
                SetMealAttendance(ref mealData, meal.Value, schemas[meal.Key]);
            }
            return mealData;
        }
        public Dictionary<string, bool> ToDto(int mealData, Dictionary<string, int>? schemas)
        {
            if (schemas == null) { throw new InvalidDataException("In DayService::IntToMeals(): parameters cannot be null"); }
            var meals = new Dictionary<string, bool>();
            foreach (var schema in schemas)
            {
                meals.Add(schema.Key, GetMealAttendance(mealData, schema.Value));
            }
            return meals;
        }
    }
}