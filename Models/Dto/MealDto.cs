namespace Rollcall.Models
{
    public class MealAttendanceDto
    {
        public int Present { get; set; }
        public bool Masked { get; set; }
    }
    public class MealDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}