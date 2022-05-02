using Rollcall.Models;
using Rollcall.Specifications;

namespace Rollcall.Services
{
    public class MealShaper
    {
        private readonly ILogger<MealShaper> _logger;
        public MealShaper(ILogger<MealShaper> logger)
        {
            _logger = logger;
        }
        public AttendanceUpdateResultDto ShapeUpdateResult<MealType>(IEnumerable<MealType> updatedAttendance) where MealType : IMeal
        {
            return new AttendanceUpdateResultDto
            {
                Meals = updatedAttendance
                .ToDictionary(m => m.MealName, m => m.Attendance)
            };
        }
        public IEnumerable<IDictionary<string, MealAttendanceDto>> MergeMealsWithMasks(IEnumerable<DailyMeal> meals,
         IEnumerable<GroupMask> masks, int year, int month)
        {
            var daysInMonth = new List<IDictionary<string, MealAttendanceDto>>();
            for (int i = 0; i < DateTime.DaysInMonth(year, month) + 1; ++i)
            {
                daysInMonth.Add(new Dictionary<string, MealAttendanceDto>());
            }
            foreach (var meal in meals)
            {
                if (daysInMonth[meal.DayOfMonth].ContainsKey(meal.MealName))
                {
                    daysInMonth[meal.DayOfMonth][meal.MealName].Present = meal.Total;
                    continue;
                }
                daysInMonth[meal.DayOfMonth].Add(meal.MealName, new MealAttendanceDto { Present = meal.Total, Masked = false });
            }
            foreach (var mask in masks)
            {
                if (daysInMonth[mask.Date.Day].ContainsKey(mask.MealName))
                {
                    daysInMonth[mask.Date.Day][mask.MealName].Masked = mask.Attendance;
                    continue;
                }
                daysInMonth[mask.Date.Day].Add(mask.MealName, new MealAttendanceDto { Present = 0, Masked = mask.Attendance });
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
        public Dictionary<string, int> ShapeMonthlySummary(IEnumerable<TotalSummaryResult> meals)
        {
            return meals.ToDictionary(a => a.MealName, a => a.Total);
        }
        public IEnumerable<MealInfoDto> ShapeInfo(IEnumerable<MealInfo> info)
        {
            return info
            .GroupBy(i => new { i.Name, i.Surname, i.ChildId, i.GroupName })
            .Select(child => new MealInfoDto
            {
                Name = child.Key.Name,
                Surname = child.Key.Surname,
                GroupName = child.Key.GroupName,
                ChildId = child.Key.ChildId,
                Summary = child.ToDictionary(m => m.MealName, m => m.Total)
            });
        }
    }
}