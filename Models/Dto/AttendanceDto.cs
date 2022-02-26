namespace Rollcall.Models
{
    public class MealDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
    public class AttendanceDto
    {
        public Dictionary<string, bool>? Meals { get; set; }
        public MealDate? Date { get; set; }
    }
}