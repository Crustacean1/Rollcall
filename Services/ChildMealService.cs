using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class ChildMealService : IMealService
    {
        MealRepository _mealRepository;
        public ChildMealService(MealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }
        public DailyAttendanceDto GetDailySummary(int id, int year, int month, int day)
        {
            return new DailyAttendanceDto { };
        }
        public IEnumerable<DailyAttendanceDto> GetDailySummaries(int id, int year, int month)
        {
            return new List<DailyAttendanceDto> { };
        }
        public MonthlyAttendanceDto GetMonthlySummary(int id, int year, int month)
        {
            return new MonthlyAttendanceDto { };
        }
        public DailyAttendanceDto UpdateSummary(MealUpdateDto dto, int id, int year, int month, int day)
        {
            var previousMeal = _mealRepository.GetChildMeal(id, new DateTime(year, month, day));
            if(previousMeal is null){
                previousMeal = new ChildAttendance{ChildId = id,};
            }
        }
    }
}