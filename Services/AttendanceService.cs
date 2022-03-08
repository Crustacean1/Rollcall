using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class AttendanceService<T>
    {
        private readonly IAttendanceRepository<T> _attendanceRepo;
        private readonly IMaskRepository<T> _maskRepo;
        private readonly ILogger<AttendanceService<T>> _logger;
        private readonly SchemaService _schemaService;
        private readonly DayAttendanceDto _defaultDay;
        private readonly AttendanceSummaryDto _defaultSummary;
        public AttendanceService(ILogger<AttendanceService<T>> logger, IAttendanceRepository<T> attendanceRepo,
        IMaskRepository<T> maskRepo, SchemaService schemaService)
        {
            _logger = logger;
            _attendanceRepo = attendanceRepo;
            _maskRepo = maskRepo;
            _schemaService = schemaService;
            _defaultDay = CreateDefaultDayAttendance();
            _defaultSummary = CreateDefaultSummary();
        }
        public DayAttendanceDto? GetAttendance(T target, int year, int month, int day)
        {
            var attendance = _attendanceRepo.GetAttendance(target, year, month, day);
            var masks = _maskRepo.GetDailyMask(target, year, month, day);
            var result = new DayAttendanceDto(_defaultDay);
            foreach (var meal in attendance)
            {
                SetAttendance(result, meal);
            }
            foreach (var mask in masks)
            {
                SetMask(result, mask);
            }
            return result;
        }
        public MonthlyAttendanceDto GetMonthlyAttendance(T target, int year, int month)
        {
            _logger.LogInformation("Getting monthly attendance");
            var meals = _attendanceRepo.GetMonthlyAttendance(target, year, month).ToList();
            var masks = _maskRepo.GetMonthlyMasks(target, year, month);
            var days = CreateMonthlyAttendance(year, month, meals, masks);
            return new MonthlyAttendanceDto
            {
                Days = days
            };
        }
        public AttendanceSummaryDto GetMonthlySummary(T target, int year, int month)
        {
            var allMealSummary = _attendanceRepo.GetMonthlySummary(target, year, month);
            var result = new AttendanceSummaryDto(_defaultSummary);
            foreach (var mealSummary in allMealSummary)
            {
                result.Meals[mealSummary.Name] = mealSummary.Attendance;
            }
            return result;
        }
        public async Task SetAttendance(T target, AttendanceRequestDto dto, int year, int month, int day)
        {
            await _attendanceRepo.SetAttendance(target, _schemaService.Translate(dto.Name), dto.Present, year, month, day);
        }
        public AttendanceSummaryDto CreateDefaultSummary()
        {
            var result = new Dictionary<string, int>();
            var keys = _schemaService.GetNames();
            foreach (var name in keys)
            {
                result[name] = 0;
            }
            return new AttendanceSummaryDto
            {
                Meals = result
            };
        }
        public DayAttendanceDto CreateDefaultDayAttendance()
        {
            var result = new Dictionary<string, MealAttendanceDto>();
            var keys = _schemaService.GetNames();
            foreach (var name in keys)
            {
                result[name] = new MealAttendanceDto { Attendance = 0, Masked = false };
            }
            return new DayAttendanceDto
            {
                Meals = result
            };
        }
        public void SetAttendance(DayAttendanceDto day, AttendanceEntity meal)
        {
            day.Meals[meal.Name].Attendance = meal.Attendance;
        }
        public void SetMask(DayAttendanceDto day, MaskEntity mask)
        {
            day.Meals[mask.Name].Masked = mask.Attendance;
        }
        public List<DayAttendanceDto> CreateMonthlyAttendance(int year, int month, IEnumerable<AttendanceEntity> attendance, IEnumerable<MaskEntity> masks)
        {
            var monthAttendance = new List<DayAttendanceDto>();
            var daysInMonth = DateTime.DaysInMonth(year, month);
            for (int i = 0; i < daysInMonth; ++i)
            {
                monthAttendance.Add(new DayAttendanceDto(_defaultDay));
            }
            foreach (var meal in attendance)
            {
                SetAttendance(monthAttendance[meal.Date.Day - 1], meal);
            }
            foreach (var mask in masks)
            {
                SetMask(monthAttendance[mask.Date.Day - 1], mask);
            }
            return monthAttendance;
        }
    }
}