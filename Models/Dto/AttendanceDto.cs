namespace Rollcall.Models
{
    public class AttendanceRequestDto
    {
        public string Name { get; set; }
        public bool Present { get; set; }
    }
    public class DayAttendanceDto
    {
        public Dictionary<string, MealAttendanceDto> Meals { get; set; }
        public DayAttendanceDto()
        {
            Meals = new Dictionary<string, MealAttendanceDto>();
        }
        public DayAttendanceDto(DayAttendanceDto attendance)
        {
            Meals = attendance.Meals.ToDictionary(a => a.Key, a => new MealAttendanceDto
            {
                Masked = a.Value.Masked,
                Present = a.Value.Present
            });
        }
    }
    public class AttendanceUpdateResultDto
    {
        public IDictionary<string, bool> Meals;
    }
    public class AttendanceCountDto
    {
        public Dictionary<string, int> Meals { get; set; }
        public AttendanceCountDto() { Meals = new Dictionary<string, int>(); }
        public AttendanceCountDto(AttendanceCountDto attendance)
        {
            Meals = new Dictionary<string, int>(attendance.Meals);
        }
    }
    public class MonthlyAttendanceDto
    {
        public List<Dictionary<string, MealAttendanceDto>> Days { get; set; }
    }
}