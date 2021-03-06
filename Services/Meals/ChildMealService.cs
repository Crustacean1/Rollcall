using Rollcall.Models;
using Rollcall.Specifications;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class ChildMealService : IMealService<Child>
    {
        private readonly IEqualityComparer<ChildMeal> _comparer;
        private readonly SummaryRepository _summaryRepo;
        private readonly MealRepository _mealRepo;
        private readonly MaskRepository _maskRepo;
        private readonly MealShaper _shaper;
        private readonly ILogger<ChildMealService> _logger;
        public ChildMealService(MealRepository mealRepo,
                                MaskRepository maskRepo,
                                SummaryRepository summaryRepo,
                                MealShaper mealShaper,
                                IEqualityComparer<ChildMeal> comparer,
                                ILogger<ChildMealService> logger)
        {
            _comparer = comparer;

            _mealRepo = mealRepo;
            _summaryRepo = summaryRepo;
            _maskRepo = maskRepo;
            _shaper = mealShaper;

            _logger = logger;
        }
        public Dictionary<string,MealAttendanceDto> GetDailySummary(Child child, int year, int month, int day)
        {
            _logger.LogInformation("Daily Meal");
            var meals = _mealRepo.GetMeals(new ChildMealSpecification(child, year, month, day))
            .Select(m => new TotalSummaryResult { MealName = m.MealName, Total = m.Attendance ? 1 : 0 });
            var masks = _maskRepo.GetMeals(new GroupMealSpecification(child, new DateTime(year, month, day)));

            return _shaper.ShapeDailySummary(meals, masks);
        }
        public IEnumerable<IDictionary<string, MealAttendanceDto>> GetDailySummaries(Child child, int year, int month)
        {
            var meals = _summaryRepo.GetMeals(new MonthlyAttendanceSpecification(child, year, month));
            var masks = _maskRepo.GetMeals(new GroupMealSpecification(child, year, month));

            return _shaper.MergeMealsWithMasks(meals, masks, year, month);
        }
        public Dictionary<string, int> GetMonthlySummary(Child child, int year, int month)
        {
            var meals = _summaryRepo.GetMeals(new MealCountSpecification(child, year, month, true));
            return _shaper.ShapeMonthlySummary(meals);
        }
        public async Task<AttendanceUpdateResultDto> UpdateAttendance(IDictionary<string, bool> updateDto, Child child, int year, int month, int day)
        {
            var dateOfUpdate = new DateTime(year, month, day);

            var currentMeals = _mealRepo.GetMeals(new ChildMealSpecification(child, year, month, day)).ToList();
            var mealUpdate = updateDto.Select(d => new ChildMeal { MealName = d.Key, Attendance = d.Value, ChildId = child.Id, Date = dateOfUpdate });

            var updatedMeals = mealUpdate.Intersect(currentMeals, _comparer).ToList();
            var newMeals = mealUpdate.Except(currentMeals, _comparer).ToList();

            _mealRepo.UpdateMeals(updatedMeals);
            _mealRepo.CreateMeals(newMeals);

            await _mealRepo.SaveChangesAsync();

            return _shaper.ShapeUpdateResult(updatedMeals.Union(newMeals));
        }
    }
}
