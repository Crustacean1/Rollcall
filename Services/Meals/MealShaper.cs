using Rollcall.Models;
using Rollcall.Specifications;

namespace Rollcall.Services
{
    public class MealShaper
    {
        public AttendanceUpdateResultDto ShapeUpdateResult<MealType>(IEnumerable<MealType> updatedAttendance, IEnumerable<MealType> createdAttendance) where MealType : IMeal
        {
            return new AttendanceUpdateResultDto
            {
                Meals = updatedAttendance
                .Union(createdAttendance)
                .ToDictionary(m => m.MealName, m => m.Attendance)
            };
        }
        public IEnumerable<DayAttendanceDto> MergeMealsWithMasks(IEnumerable<DailyMeal> meals,
         IEnumerable<GroupMask> masks, int year, int month)
        {
            var daysInMonth = new List<DayAttendanceDto>();
            for (int i = 0; i < DateTime.DaysInMonth(year, month); ++i)
            {
                daysInMonth.Add(new DayAttendanceDto());
            }
            foreach (var meal in meals)
            {
                if (daysInMonth[meal.DayOfMonth].Meals.ContainsKey(meal.MealName))
                {
                    daysInMonth[meal.DayOfMonth].Meals[meal.MealName].Present = meal.Total;
                    continue;
                }
                daysInMonth[meal.DayOfMonth].Meals.Add(meal.MealName, new MealAttendanceDto { Present = meal.Total, Masked = false });
            }
            foreach (var mask in masks)
            {
                if (daysInMonth[mask.Date.Day].Meals.ContainsKey(mask.MealName))
                {
                    daysInMonth[mask.Date.Day].Meals[mask.MealName].Masked = mask.Attendance;
                    continue;
                }
                daysInMonth[mask.Date.Day].Meals.Add(mask.MealName, new MealAttendanceDto { Present = 0, Masked = mask.Attendance });
            }
            return daysInMonth;
        }
        public DayAttendanceDto ShapeDailySummary(IEnumerable<TotalSummaryResult> meals, IEnumerable<GroupMask> masks)
        {
            var result = meals.ToDictionary(m => m.MealName, m => new MealAttendanceDto { Present = m.Total, Masked = false });
            foreach (var mask in masks)
            {
                result[mask.MealName].Masked = mask.Attendance;
            }
            return new DayAttendanceDto { Meals = result };
        }
        public AttendanceCountDto ShapeMonthlySummary(IEnumerable<TotalSummaryResult> meals)
        {
            return new AttendanceCountDto { Meals = meals.ToDictionary(a => a.MealName, a => a.Total) };
        }
    }
}