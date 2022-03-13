using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class DtoShapingService
    {
        private readonly ILogger<DtoShapingService> _logger;
        private readonly SchemaService _schemaService;
        private readonly DayAttendanceDto _defaultDay;
        private readonly AttendanceSummaryDto _defaultSummary;
        public DtoShapingService(ILogger<DtoShapingService> logger, SchemaService schemaService)
        {
            _logger = logger;
            _schemaService = schemaService;
            _defaultDay = CreateDefaultDayAttendance();
            _defaultSummary = CreateDefaultSummary();
        }
        public AttendanceSummaryDto CreateMonthlySummary(IEnumerable<AttendanceEntity> attendance)
        {
            var result = GetDefaultSummary();
            foreach (var meal in attendance)
            {
                result.Meals[meal.Name] = meal.Attendance;
            }
            return result;
        }
        public DayAttendanceDto CreateDailyAttendance(IEnumerable<AttendanceEntity> attendance, IEnumerable<MaskEntity> masks)
        {
            var result = GetDefaultDayAttendance();
            foreach (var meal in attendance)
            {
                SetAttendanceDto(result, meal);
            }
            foreach (var mask in masks)
            {
                SetMaskDto(result, mask);
            }
            return result;
        }
        public MonthlyAttendanceDto CreateMonthlyAttendance(int year, int month, IEnumerable<AttendanceEntity> attendance, IEnumerable<MaskEntity> masks)
        {
            var monthAttendance = new List<DayAttendanceDto>();
            var daysInMonth = DateTime.DaysInMonth(year, month);
            for (int i = 0; i < daysInMonth; ++i)
            {
                monthAttendance.Add(GetDefaultDayAttendance());
            }
            foreach (var meal in attendance)
            {
                SetAttendanceDto(monthAttendance[meal.Date.Day - 1], meal);
            }
            foreach (var mask in masks)
            {
                SetMaskDto(monthAttendance[mask.Date.Day - 1], mask);
            }
            return new MonthlyAttendanceDto
            {
                Days = monthAttendance.Select(a => a.Meals).ToList()
            };
        }
        private void SetAttendanceDto(DayAttendanceDto day, AttendanceEntity meal)
        {
            day.Meals[meal.Name].Attendance = meal.Attendance;
        }
        private void SetMaskDto(DayAttendanceDto day, MaskEntity mask)
        {
            day.Meals[mask.Name].Masked = mask.Attendance;
        }
        private AttendanceSummaryDto GetDefaultSummary()
        {
            return new AttendanceSummaryDto(_defaultSummary);
        }
        private DayAttendanceDto GetDefaultDayAttendance()
        {
            return new DayAttendanceDto(_defaultDay);
        }
        private AttendanceSummaryDto CreateDefaultSummary()
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
        private DayAttendanceDto CreateDefaultDayAttendance()
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
    }
}