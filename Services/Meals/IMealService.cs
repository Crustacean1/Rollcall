using Rollcall.Models;

namespace Rollcall.Services
{
    //Common base
    public interface IMealService<TargetType>
    {
        public DayAttendanceDto GetDailySummary(TargetType target, int year, int month, int day);
        public IEnumerable<DayAttendanceDto> GetDailySummaries(TargetType target, int year, int month);
        public AttendanceCountDto GetMonthlySummary(TargetType target, int year, int month);
        public Task<AttendanceUpdateResultDto> UpdateAttendance(IDictionary<string, bool> updateDto, TargetType target, int year, int month, int day);
    }
}