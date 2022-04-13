namespace Rollcall.Services
{
    //Common base
    public class DailyAttendanceDto
    {

    }
    public class MonthlyAttendanceDto
    {

    }
    public class MealUpdateDto
    {

    }
    public interface IMealService
    {
        public DailyAttendanceDto GetDailySummary(int id, int year, int month, int day);
        public IEnumerable<DailyAttendanceDto> GetDailySummaries(int id, int year, int month);
        public MonthlyAttendanceDto GetMonthlySummary(int id, int year, int month);
        public DailyAttendanceDto UpdateSummary(MealUpdateDto dto, int id, int year, int month, int day);
    }
}