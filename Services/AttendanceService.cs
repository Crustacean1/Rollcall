using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class AttendanceService<T>
    {
        private readonly IAttendanceRepository<T> _attendanceRepo;
        private readonly ILogger<AttendanceService<T>> _logger;
        public AttendanceService(ILogger<AttendanceService<T>> logger, IAttendanceRepository<T> attendanceRepo)
        {
            _logger = logger;
            _attendanceRepo = attendanceRepo;
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
        public async Task SetAttendance(T target, AttendanceRequestDto dto, int year, int month, int day)
        {
            await _attendanceRepo.SetAttendance(target, dto, year, month, day);
        }
    }
}