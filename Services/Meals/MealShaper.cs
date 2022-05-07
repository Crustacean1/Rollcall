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
        public Dictionary<string, MealAttendanceDto> ShapeDailySummary(IEnumerable<TotalSummaryResult> meals, IEnumerable<GroupMask> masks)
        {
            var result = meals.ToDictionary(m => m.MealName, m => new MealAttendanceDto { Present = m.Total, Masked = false });
            foreach (var mask in masks)
            {
                if (result.ContainsKey(mask.MealName))
                {
                    result[mask.MealName].Masked = mask.Attendance;
                    continue;
                }
                else
                {
                    result.Add(mask.MealName, new MealAttendanceDto { Present = 0, Masked = mask.Attendance });
                }
            }
            return result;
        }
        public Dictionary<string, int> ShapeMonthlySummary(IEnumerable<TotalSummaryResult> meals)
        {
            return meals.ToDictionary(a => a.MealName, a => a.Total);
        }
        public IEnumerable<GroupMealInfoDto> ShapeDailyInfo(IEnumerable<MealInfo> info, IEnumerable<GroupMask> masks)
        {
            return info
            .GroupBy(o => new { o.GroupName, o.GroupId })
            .Select(group => new GroupMealInfoDto
            {
                GroupName = group.Key.GroupName,
                GroupId = group.Key.GroupId,
                Children = group.GroupBy(m => new { m.ChildId, m.Name, m.Surname })
                .Select(
                m => new MealInfoDto
                {
                    Name = m.Key.Name,
                    Surname = m.Key.Surname,
                    ChildId = m.Key.ChildId,
                    GroupName = group.Key.GroupName,
                    Meals = m.ToDictionary(a => a.MealName, a => a.Total)
                }),
                Masks = masks.Where(m => m.GroupId == group.Key.GroupId)
                .ToDictionary(m => m.MealName, m => m.Attendance)
            });
        }
        public IEnumerable<MealInfoDto> ShapeMonthlyInfo(IEnumerable<MealInfo> info)
        {
            return info
            .GroupBy(i => new { i.Name, i.Surname, i.ChildId, i.GroupName })
            .Select(child => new MealInfoDto
            {
                Name = child.Key.Name,
                Surname = child.Key.Surname,
                ChildId = child.Key.ChildId,
                GroupName = child.Key.GroupName,
                Meals = child.ToDictionary(m => m.MealName, m => m.Total)
            });
        }
    }
}