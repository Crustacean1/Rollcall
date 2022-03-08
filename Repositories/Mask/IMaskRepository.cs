using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class MaskEntity
    {
        public string Name { get; set; }
        public bool Attendance { get; set; }
        public MealDate Date { get; set; }
    }
    public interface IMaskRepository<T>
    {
        IEnumerable<MaskEntity> GetMonthlyMasks(T target, int year, int month);
        IEnumerable<MaskEntity> GetDailyMask(T target, int year, int month, int day);
    }
}