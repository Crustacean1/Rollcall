using Rollcall.Models;

namespace Rollcall.Repositories
{
    public interface IAttendanceRepository<T>
    {
        public IEnumerable<AttendanceEntity> GetAttendance(T target, int year, int month, int day);
        public IEnumerable<AttendanceEntity> GetMonthlyAttendance(T target, int year, int month);
        public IEnumerable<AttendanceEntity> GetMonthlySummary(T target, int year, int month);
        public Task SetAttendance(T target, int mealId, bool attendance, int year, int month, int day);
    }
}