using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class ChildMealService : IMealService
    {
        private ChildAttendanceComparer _comparer;
        private readonly MealRepository _mealRepo;
        private readonly SummaryRepository _summaryRepo;
        private readonly ILogger<ChildMealService> _logger;
        public ChildMealService(MealRepository mealRepository, SummaryRepository summaryRepository, ILogger<ChildMealService> logger)
        {
            _comparer = new ChildAttendanceComparer();
            _mealRepo = mealRepository;
            _summaryRepo = summaryRepository;
            _logger = logger;
        }
        public DayAttendanceDto GetDailySummary(int id, int year, int month, int day)
        {
            return new DayAttendanceDto { };
        }
        public IEnumerable<DayAttendanceDto> GetDailySummaries(int id, int year, int month)
        {
            return new List<DayAttendanceDto> { };
        }
        public AttendanceCountDto GetMonthlySummary(int id, int year, int month)
        {
            var result = _summaryRepo.GetGroupSummary(new ChildSummarySpecification(id));
            return new AttendanceCountDto { };
        }
        public async Task<AttendanceUpdateResultDto> UpdateAttendance(IDictionary<string, bool> updateDto, int id, int year, int month, int day)
        {
            //TODO: Optimize updating so it doesnt update values that stay the same
            var currentDate = new DateTime(year, month, day);
            var currentMeals = _mealRepo.GetChildAttendance(id, new DateTime(year, month, day)).ToList();
            var mealUpdate = updateDto.Select(d => new ChildAttendance { MealName = d.Key, Attendance = d.Value, ChildId = id, Date = currentDate });

            var updatedMeals = mealUpdate.Intersect(currentMeals, _comparer).ToList();
            var newMeals = mealUpdate.Except(currentMeals, _comparer).ToList();

            _mealRepo.UpdateChildAttendance(updatedMeals);
            _mealRepo.AddChildAttendance(newMeals);

            await _mealRepo.SaveChangesAsync();

            return new AttendanceUpdateResultDto
            {
                Meals = updatedMeals
            .Union(newMeals)
            .ToDictionary(m => m.MealName, m => m.Attendance)
            };
        }
    }
    class ChildAttendanceComparer : IEqualityComparer<ChildAttendance>
    {
        public bool Equals(ChildAttendance? a, ChildAttendance? b)
        {
            return a is not null && b is not null && a.MealName == b.MealName;
        }
        public int GetHashCode(ChildAttendance a)
        {
            return a.MealName.GetHashCode() ^ a.Date.GetHashCode() ^ a.ChildId.GetHashCode();
        }
    }
}