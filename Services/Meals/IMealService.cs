using Rollcall.Models;

namespace Rollcall.Services
{
    //Common base
    public interface IMealService
    {
        public DayAttendanceDto GetDailySummary(int id, int year, int month, int day);
        public IEnumerable<DayAttendanceDto> GetDailySummaries(int id, int year, int month);
        public AttendanceCountDto GetMonthlySummary(int id, int year, int month);
        public Task<AttendanceUpdateResultDto> UpdateAttendance(IDictionary<string, bool> updateDto, int id, int year, int month, int day);
    }
}