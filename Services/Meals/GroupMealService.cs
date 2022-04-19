using Rollcall.Models;
using Rollcall.Repositories;
using Rollcall.Specifications;

namespace Rollcall.Services
{
    /*public class GroupMealService : IGroupMealService
    {
        private readonly IEqualityComparer<GroupMask> _comparer;
        private readonly SummaryRepository _summaryRepo;
        private readonly MealRepository<ChildMeal> _mealRepo;
        private readonly MealRepository<GroupMask> _maskRepo;
        private readonly MealShaper _shaper;
        private readonly ILogger<GroupMealService> _logger;
        public GroupMealService(MealRepository<ChildMeal> mealRepo,
                                MealRepository<GroupMask> maskRepo,
                                SummaryRepository summaryRepo,
                                MealShaper mealShaper,
                                IEqualityComparer<GroupMask> comparer,
                                ILogger<GroupMealService> logger)
        {
            _comparer = comparer;

            _mealRepo = mealRepo;
            _summaryRepo = summaryRepo;
            _maskRepo = maskRepo;
            _shaper = mealShaper;

            _logger = logger;
        }
        public DayAttendanceDto GetDailySummary(Group group, int year, int month, int day)
        {
            var meals = _summaryRepo.GetMeals(new TotalSummarySpecification(group, year, month, day, true));
            var masks = _maskRepo.GetMeals(new GroupMealSpecification(group, year, month, day));
            return _shaper.ShapeDailySummary(meals, masks);
        }
        public IEnumerable<DayAttendanceDto> GetDailySummaries(Group group, int year, int month)
        {
            var meals = _summaryRepo.GetMeals(new MonthlyAttendanceSpecification(group, year, month));
            var masks = _maskRepo.GetMeals(new GroupMealSpecification(group, year, month));

            return _shaper.MergeMealsWithMasks(meals, masks, year, month);
        }
        public AttendanceCountDto GetMonthlySummary(Group group, int year, int month)
        {
            var meals = _summaryRepo.GetMeals(new TotalSummarySpecification(group, year, month, true));
            return _shaper.ShapeMonthlySummary(meals);
        }
        public async Task<AttendanceUpdateResultDto> UpdateAttendance(IDictionary<string, bool> updateDto, Group group, int year, int month, int day)
        {
            var dateOfUpdate = new DateTime(year, month, day);

            var currentMeals = _maskRepo.GetMeals(new GroupMealSpecification(group, year, month, day)).ToList();
            var mealUpdate = updateDto.Select(d => new GroupMask { MealName = d.Key, Attendance = d.Value, GroupId = group.Id, Date = dateOfUpdate });

            var updatedMeals = mealUpdate.Intersect(currentMeals, _comparer).ToList();
            var newMeals = mealUpdate.Except(currentMeals, _comparer).ToList();

            _maskRepo.UpdateMeals(updatedMeals);
            _maskRepo.CreateMeals(newMeals);

            await _maskRepo.SaveChangesAsync();

            return _shaper.ShapeUpdateResult(updatedMeals, newMeals);
        }
    }*/
}