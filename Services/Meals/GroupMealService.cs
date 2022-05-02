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

            var maskSpecification = group is null ? new GroupMealSpecification(new DateTime(year, month, day)) :
             new GroupMealSpecification(group, new DateTime(year, month, day));
            var masks = _maskRepo.GetMeals(maskSpecification);

            return _shaper.ShapeDailySummary(meals, masks);
        }
        public IEnumerable<IDictionary<string, MealAttendanceDto>> GetDailySummaries(Group? group, int year, int month)
        {
            var mealSpecification = group is null ? new MonthlyAttendanceSpecification(year, month) : new MonthlyAttendanceSpecification(group, year, month);
            var meals = _summaryRepo.GetMeals(mealSpecification);

            var maskSpecification = group is null ? new GroupMealSpecification(year, month) : new GroupMealSpecification(group, year, month);
            var masks = _maskRepo.GetMeals(maskSpecification);

            return _shaper.MergeMealsWithMasks(meals, masks, year, month);
        }
        public Dictionary<string, int> GetMonthlySummary(Group? group, int year, int month)
        {
            var mealSpec = group is null ? new TotalSummarySpecification(year, month, true) : new TotalSummarySpecification(group, year, month, true);
            var meals = _summaryRepo.GetMeals(mealSpec);
            return _shaper.ShapeMonthlySummary(meals);
        }
        public async Task<AttendanceUpdateResultDto> UpdateAttendance(IDictionary<string, bool> updateDto, Group? group, int year, int month, int day)
        {
            var dateOfUpdate = new DateTime(year, month, day);
            var groupList = new List<Group>();
            GroupMealSpecification spec;
            if (group is null)
            {
                spec = new GroupMealSpecification(dateOfUpdate);
                groupList = _groupRepo.GetGroups().ToList();
            }
            else
            {
                spec = new GroupMealSpecification(group, dateOfUpdate);
                groupList.Add(group);
            }

            var mealUpdate = updateDto.Select(u => new GroupMask { MealName = u.Key, Attendance = u.Value });
            var fullMealUpdate = mealUpdate.Join(groupList, a => true, a => true, (u, g) => new GroupMask
            {
                MealName = u.MealName,
                Attendance = u.Attendance,
                Date = dateOfUpdate,
                GroupId = g.Id
            });

            UpdateGroupAttendance(spec, fullMealUpdate, dateOfUpdate);

            await _maskRepo.SaveChangesAsync();

            return _shaper.ShapeUpdateResult(fullMealUpdate, new List<GroupMask>());//Dirty hack for now, does not reflect actual db state
        }
        private void UpdateGroupAttendance(GroupMealSpecification spec, IEnumerable<GroupMask> update, DateTime dateOfUpdate)
        {
            var currentMeals = _maskRepo.GetMeals(spec).ToList();

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