using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class AttendanceService<T>
    {
        private readonly IAttendanceRepository<T> _attendanceRepo;
        private readonly IMaskRepository<T> _maskRepo;
        private readonly ILogger<AttendanceService<T>> _logger;
        public AttendanceService(ILogger<AttendanceService<T>> logger, IAttendanceRepository<T> attendanceRepo, IMaskRepository<T> maskRepo)
        {
            _logger = logger;
            _attendanceRepo = attendanceRepo;
            _maskRepo = maskRepo;
        }
        private AttendanceDto JoinAttendance(AttendanceData mask, AttendanceSummary summary)
        {
            return new AttendanceDto
            {
                Date = mask.Date,
                Masks = mask.Meals,
                Meals = summary.Meals
            };
        }
        public AttendanceDto? GetAttendance(T target, int year, int month, int day)
        {
            var summary = _attendanceRepo.GetAttendance(target, year, month, day);
            return summary;
        }
        public IEnumerable<AttendanceDto> GetMonthlyAttendance(T target, int year, int month)
        {
            _logger.LogInformation("Getting monthly attendance");
            var meals = _attendanceRepo.GetMonthlyAttendance(target, year, month).ToList();
            return meals;
        }
        public AttendanceDto? GetMonthlySummary(T target, int year, int month)
        {
            var meal = _attendanceRepo.GetMonthlySummary(target, year, month);
            return meal;
        }
        public async Task SetAttendance(T target, AttendanceData data)
        {
            await _maskRepo.SetMeal(target, data);
        }
    }
}