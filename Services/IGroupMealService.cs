using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class DailyMealInfoDto
    {

    }
    public class MonthlyMealInfoDto
    {

    }
    public interface IGroupMealService
    {
        public IEnumerable<DailyMealInfoDto> GetDailyInfo(int groupId, int year, int month, int day);
        public MonthlyMealInfoDto GetMonthlyInfo(int groupId, int year, int month, int day);
        public int ExtendDefaultAttendance(int year, int month);
    }
}