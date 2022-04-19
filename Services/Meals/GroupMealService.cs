using Rollcall.Models;
using Rollcall.Repositories;
using Rollcall.Specifications;

namespace Rollcall.Services
{
    public class GroupMealService : IGroupMealService
    {
        private readonly IEqualityComparer<GroupMask> _comparer;
        private readonly GroupRepository _groupRepo;
        private readonly SummaryRepository _summaryRepo;
        private readonly MealRepository<ChildMeal> _mealRepo;
        private readonly MealRepository<GroupMask> _maskRepo;
        private readonly MealShaper _shaper;
        private readonly ILogger<GroupMealService> _logger;
        public GroupMealService(MealRepository<ChildMeal> mealRepo,
                                MealRepository<GroupMask> maskRepo,
                                GroupRepository groupRepo,
                                SummaryRepository summaryRepo,
                                MealShaper mealShaper,
                                IEqualityComparer<GroupMask> comparer,
                                ILogger<GroupMealService> logger)
        {
            _comparer = comparer;

            _groupRepo = groupRepo;
            _mealRepo = mealRepo;
            _summaryRepo = summaryRepo;
            _maskRepo = maskRepo;
            _shaper = mealShaper;

            _logger = logger;
        }
        public DayAttendanceDto GetDailySummary(Group? group, int year, int month, int day)
        {
            var mealSpecification = group is null ? new TotalSummarySpecification(year, month, day, true) : new TotalSummarySpecification(group, year, month, day, false);
            var meals = _summaryRepo.GetMeals(mealSpecification);

            var maskSpecification = group is null ? new GroupMealSpecification(year, month, day) : new GroupMealSpecification(group, year, month, day);
            var masks = _maskRepo.GetMeals(maskSpecification);

            return _shaper.ShapeDailySummary(meals, masks);
        }
        public IEnumerable<DayAttendanceDto> GetDailySummaries(Group? group, int year, int month)
        {
            var mealSpecification = group is null ? new MonthlyAttendanceSpecification(year, month) : new MonthlyAttendanceSpecification(group, year, month);
            var meals = _summaryRepo.GetMeals(mealSpecification);

            var maskSpecification = group is null ? new GroupMealSpecification(year, month) : new GroupMealSpecification(group, year, month);
            var masks = _maskRepo.GetMeals(maskSpecification);

            return _shaper.MergeMealsWithMasks(meals, masks, year, month);
        }
        public AttendanceCountDto GetMonthlySummary(Group? group, int year, int month)
        {
            var mealSpec = group is null ? new TotalSummarySpecification(year, month, true) : new TotalSummarySpecification(group, year, month, true);
            var meals = _summaryRepo.GetMeals(mealSpec);
            return _shaper.ShapeMonthlySummary(meals);
        }
        public async Task<AttendanceUpdateResultDto> UpdateAttendance(IDictionary<string, bool> updateDto, Group? group, int year, int month, int day)
        {
            var dateOfUpdate = new DateTime(year, month, day);
            var mealUpdate = updateDto.Select(d => new GroupMask { MealName = d.Key, Attendance = d.Value, GroupId = group.Id, Date = dateOfUpdate });

            if (group is null)
            {
                var groups = _groupRepo.GetGroups();
                foreach (var subGroup in groups)
                {
                    UpdateGroupAttendance(subGroup, mealUpdate, dateOfUpdate);
                }
            }
            else
            {
                UpdateGroupAttendance(group, mealUpdate, dateOfUpdate);
            }

            await _maskRepo.SaveChangesAsync();

            return _shaper.ShapeUpdateResult(mealUpdate, new List<GroupMask>());//Dirty hack for now, does not reflect actual db state
        }
        private void UpdateGroupAttendance(Group group, IEnumerable<GroupMask> update, DateTime dateOfUpdate)
        {
            var currentMeals = _maskRepo.GetMeals(new GroupMealSpecification(group, dateOfUpdate.Year, dateOfUpdate.Month, dateOfUpdate.Day)).ToList();

            var updatedMeals = update.Intersect(currentMeals, _comparer).ToList();
            var newMeals = update.Except(currentMeals, _comparer).ToList();

            _maskRepo.UpdateMeals(updatedMeals);
            _maskRepo.CreateMeals(newMeals);
        }
        public IEnumerable<MealInfoDto> GetDailyInfo(Group? group, int year, int month, int day)
        {
            var specification = group is null ? new GroupInfoSpecification(year, month, day) : new GroupInfoSpecification(group, year, month, day);
            var dailyInfo = _summaryRepo.GetMealInfo(specification);
            return _shaper.ShapeInfo(dailyInfo);
        }
        public IEnumerable<MealInfoDto> GetMonthlyInfo(Group? group, int year, int month)
        {
            var specification = group is null ? new GroupInfoSpecification(year, month) : new GroupInfoSpecification(group, year, month);
            var monthlyInfo = _summaryRepo.GetMealInfo(specification);
            return _shaper.ShapeInfo(monthlyInfo);
        }
        public int ExtendDefaultAttendance(int year, int month)
        {
            return 0;
        }
    }
}