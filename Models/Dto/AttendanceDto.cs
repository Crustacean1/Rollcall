namespace Rollcall.Models
{
    public class DayAttendanceDto
    {
        public Dictionary<string, MealAttendanceDto> Meals { get; set; }
        public DayAttendanceDto()
        {
            Meals = new Dictionary<string, MealAttendanceDto>();
        }
    }
    public class AttendanceCountDto
    {
        public Dictionary<string, int> Meals { get; set; }
        public AttendanceCountDto() { Meals = new Dictionary<string, int>(); }
    }
}