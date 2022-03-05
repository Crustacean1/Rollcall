using Rollcall.Models;

namespace Rollcall.Repositories
{
    public interface IAttendanceRepository<T>
    {
        public AttendanceDto? GetAttendance(T target, int year, int month, int day);
        public IEnumerable<AttendanceDto> GetMonthlyAttendance(T target, int year, int month);
        public AttendanceDto? GetMonthlySummary(T target, int year, int month);

        public Task SetAttendance(T target,AttendanceRequestDto attendance,int year,int month,int day);
    }
}