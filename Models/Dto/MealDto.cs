namespace Rollcall.Models
{
    public class MealAttendanceDto
    {
        public int Present { get; set; }
        public bool Masked { get; set; }
        public MealAttendanceDto()
        {
            Present = 0;
            Masked = false;
        }
    }
    public class MealDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public MealDate() { }
        public MealDate(int year, int month, int day = 0)
        {
            Year = year;
            Month = month;
            Day = day;
        }
    }
}