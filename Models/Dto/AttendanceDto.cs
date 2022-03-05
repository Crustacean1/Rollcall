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
        public Dictionary<string, MealDto> Attendance { get; set; }
        public MealDate? Date { get; set; }
    }
    public class AttendanceRequestDto
    {
        public string Name { get; set; }
        public bool Present { get; set; }
    }
}