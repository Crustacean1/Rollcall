using Rollcall.Models;

namespace Rollcall.Services
{
    public class DailyMealInfoDto
    {

    }
    public class MonthlyMealInfoDto
    {

    }
    public interface IGroupMealService : IMealService<Group?>
    {
        public IEnumerable<DailyMealInfoDto> GetDailyInfo(Group? group, int year, int month, int day);
        public MonthlyMealInfoDto GetMonthlyInfo(Group? group, int year, int month, int day);
        public int ExtendDefaultAttendance(int year, int month);
    }
}