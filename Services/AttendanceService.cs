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
        public AttendanceService(ILogger<AttendanceService<T>> logger, IAttendanceRepository<T> attendanceRepo,
        IMaskRepository<T> maskRepo, SchemaService schemaService)
        {
            _logger = logger;
            _attendanceRepo = attendanceRepo;
            _maskRepo = maskRepo;
            _schemaService = schemaService;
        }
        public AttendanceDto? GetAttendance(T target, int year, int month, int day)
        {
            var summary = _attendanceRepo.GetAttendance(target, year, month, day);
            return new AttendanceDto
            {
                Attendance = ParseDayToDict(summary),
                Date = new MealDate { Year = year, Month = month, Day = day }
            };
        }
        public Dictionary<int, Dictionary<string, MealDto>> GetMonthlyAttendance(T target, int year, int month)
        {
            _logger.LogInformation("Getting monthly attendance");
            var meals = _attendanceRepo.GetMonthlyAttendance(target, year, month).ToList();
            var masks = _maskRepo.GetMonthlyMasks(target, year, month);

            var attendance = FullJoin(meals, masks);
            var parsedMeals = attendance
            .GroupBy(m => new { m.Date.Day, m.Name })
            .Select(a => new AttendanceDto
            {
                Date = new MealDate { Year = year, Month = month, Day = a.Key.Day },
                Attendance = ParseDayToDict(a)
            });
            return ParseDayToMonth(parsedMeals);
        }
        public AttendanceDto? GetMonthlySummary(T target, int year, int month)
        {
            var meal = _attendanceRepo.GetMonthlySummary(target, year, month).Select();
            return new AttendanceSummaryDto
            {
                Attendance = ParseDayToDict(meal),
            };
        }
        public async Task SetAttendance(T target, AttendanceRequestDto dto, int year, int month, int day)
        {
            await _attendanceRepo.SetAttendance(target, _schemaService.Translate(dto.Name), dto.Present, year, month, day);
        }
        private Dictionary<string, MealDto> ParseDayToDict(IEnumerable<FullAttendanceEntity> attendance)
        {
            return attendance.ToDictionary(c => c.Name, c => new MealDto { Attendance = c.Attendance, Masked = false });
        }
        private Dictionary<int, Dictionary<string, MealDto>> ParseDayToMonth(IEnumerable<AttendanceDto> attendance)
        {
            return attendance.ToDictionary(a => a.Date.Day, a => a.Attendance);
        }
        private IEnumerable<FullAttendanceEntity> FullJoin(IEnumerable<AttendanceEntity> attendance, IEnumerable<MaskEntity> masks)
        {
            var leftJoin = attendance.SelectMany(a => masks.Where(m => a.Name == m.Name && a.Date.Day == m.Date.Day).DefaultIfEmpty(), (a, m) => new FullAttendanceEntity
            {
                Masked = m == null ? false : m.Attendance,
                Attendance = a.Attendance,
                Name = a.Name,
                Date = a.Date
            });
            var rightJoin = masks.SelectMany(m => attendance.Where(a => a.Name == m.Name && a.Date.Day == m.Date.Day).DefaultIfEmpty(), (m, a) => new FullAttendanceEntity
            {
                Masked = m.Attendance,
                Attendance = a == null ? 0 : a.Attendance,
                Name = m.Name,
                Date = m.Date
            });
            return leftJoin.Union(rightJoin);
        }
    }
    class FullAttendanceEntity : IEquatable<FullAttendanceEntity>
    {
        public bool Masked { get; set; }
        public int Attendance { get; set; }
        public MealDate Date { get; set; }
        public string Name { get; set; }

        public bool Equals(FullAttendanceEntity b)
        {
            if (b.Date == null) { return false; }
            return Date.Day == b.Date.Day && Name == b.Name;
        }
    }
}