using Rollcall.Models;
namespace Rollcall.Services
{
    public class AttendanceHandlerService
    {
        private void SetMealAttendance(ref int mealData, bool attendance, int mealId)
        {
            var mask = ((attendance ? 1 : 0) << mealId);
            mealData = (mealData & (~mask)) ^ mask;
        }
        private bool GetMealAttendance(int mealData, int mealId)
        {
            return ((mealData >> mealId) & 1) == 1;
        }

        public int FromDto(ICollection<MealDto>? meals, ICollection<MealSchema>? schemas)
        {
            if (schemas == null || meals == null) { throw new InvalidDataException("In AttendanceHandlerService::MealsToInt(): parameters cannot be null"); }
            int mealData = 0;
            foreach (var meal in meals)
            {
                var mealSchema = schemas.Where((s) => s.Name == meal.Name).FirstOrDefault();
                if (mealSchema == null)
                {
                    throw new InvalidDataException($"In AttendanceHandlerService::ConvertMealData(): Invalid meal property name: {meal.Name}");
                }
                SetMealAttendance(ref mealData, meal.Present, mealSchema.Id);
            }
            return mealData;
        }
        public ICollection<MealDto> ToDto(int mealData, ICollection<MealSchema>? schemas)
        {
            if (schemas == null) { throw new InvalidDataException("In DayService::IntToMeals(): parameters cannot be null"); }
            var meals = new List<MealDto>();
            foreach (var schema in schemas)
            {
                meals.Add(new MealDto
                {
                    Name = schema.Name,
                    Present = GetMealAttendance(mealData, schema.Id),
                });
            }
            return meals;
        }
        public Attendance FromDto(AttendanceDto dto, ICollection<MealSchema> schema)
        {
            var result = new Attendance();
            result.ChildId = dto.ChildId;
            result.Date = new DateTime(dto.Year, dto.Month, dto.Day);
            result.Meals = FromDto(dto.Meals, schema);
            return result;
        }
        public AttendanceDto ToDto(Attendance day, ICollection<MealSchema> schemas)
        {
            return new AttendanceDto
            {
                ChildId = day.ChildId,
                Year = day.Date.Year,
                Month = day.Date.Month,
                Day = day.Date.Day,
                Meals = ToDto(day.Meals,schemas)
            };
        }
    }
}