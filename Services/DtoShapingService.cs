using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class DtoShapingService
    {
        private readonly ILogger<DtoShapingService> _logger;
        private readonly DayAttendanceDto _defaultDay;
        private readonly AttendanceCountDto _defaultCount;
        public DtoShapingService(ILogger<DtoShapingService> logger)
        {
            _logger = logger;
            _defaultDay = CreateDefaultDayAttendance();
            _defaultCount = CreateDefaultCount();
        }
        public AttendanceCountDto CreateMonthlyCount(IEnumerable<AttendanceEntity> attendance)
        {
            var result = GetDefaultCount();
            foreach (var meal in attendance)
            {
                result.Meals[meal.Name] = meal.Present;
            }
            return result;
        }
        public IEnumerable<ChildAttendanceSummaryDto> CreateMonthlySummary(IEnumerable<ChildAttendanceEntity> attendance)
        {
            return attendance.GroupBy(a => new { a.ChildId }).Select(c => new ChildAttendanceSummaryDto
            {
                Name = c.First().Name,
                Surname = c.First().Surname,
                GroupName = c.First().GroupName,
                ChildId = c.Key.ChildId,
                Summary = c.ToDictionary(m => m.MealName, m => m.Present)
            });
        }
        public IEnumerable<DailyChildAttendanceDto> CreateDailySummary(IEnumerable<ChildAttendanceEntity> attendance, IEnumerable<MaskEntity> masks)
        {
            var maskLookup = masks.ToDictionary(m => m.Name, m => m.Masked);
            return attendance.GroupBy(a => new { a.ChildId, a.Name, a.Surname, a.GroupName })
            .Select(c => new DailyChildAttendanceDto
            {
                Name = c.Key.Name,
                Surname = c.Key.Surname,
                GroupName = c.Key.GroupName,
                ChildId = c.Key.ChildId,
                Meals = c.ToDictionary(p => p.MealName, p => new MealAttendanceDto
                {
                    Present = p.Present,
                    Masked = maskLookup.ContainsKey(p.MealName) ? maskLookup[p.MealName] : false
                })
            });
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
            day.Meals[meal.Name].Present = meal.Present;
        }
        private void SetMaskDto(DayAttendanceDto day, MaskEntity mask)
        {
            day.Meals[mask.Name].Masked = mask.Masked;
        }
        private AttendanceCountDto GetDefaultCount()
        {
            return new AttendanceCountDto(_defaultCount);
        }
        private DayAttendanceDto GetDefaultDayAttendance()
        {
            return new DayAttendanceDto(_defaultDay);
        }
        private AttendanceCountDto CreateDefaultCount()
        {
            var result = new Dictionary<string, int>();
            var keys = new List<string> { "j", "p", "2" };//_schemaService.GetNames();
            foreach (var name in keys)
            {
                result[name] = 0;
            }
            return new AttendanceCountDto
            {
                Meals = result
            };
        }
        private DayAttendanceDto CreateDefaultDayAttendance()
        {
            var result = new Dictionary<string, MealAttendanceDto>();
            var keys = new List<string> { "j", "p", "2" };//_schemaService.GetNames();
            foreach (var name in keys)
            {
                result[name] = new MealAttendanceDto { Present = 0, Masked = false };
            }
            return new DayAttendanceDto
            {
                Meals = result
            };
        }
    }
}