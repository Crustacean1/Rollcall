using Rollcall.Models;

namespace Rollcall.Services
{
    public class MonthlyMealInfoDto
    {

    }
    public interface IGroupMealService : IMealService<Group?>
    {
        public IEnumerable<MealInfoDto> GetDailyInfo(Group? group, int year, int month, int day);
        public IEnumerable<MealInfoDto> GetMonthlyInfo(Group? group, int year, int month);
        public int ExtendDefaultAttendance(int year, int month);
    }
}