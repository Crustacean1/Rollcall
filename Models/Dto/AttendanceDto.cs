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
        public DayAttendanceDto() { }
        public DayAttendanceDto(DayAttendanceDto attendance)
        {
            Meals = attendance.Meals.ToDictionary(a => a.Key, a => new MealAttendanceDto
            {
                Masked = a.Value.Masked,
                Attendance = a.Value.Attendance
            });
        }
    }
    public class AttendanceSummaryDto
    {
        public Dictionary<string, int> Meals { get; set; }
        public AttendanceSummaryDto() { }
        public AttendanceSummaryDto(AttendanceSummaryDto attendance)
        {
            Meals = new Dictionary<string, int>(attendance.Meals);
        }
    }
    public class MonthlyAttendanceDto
    {
        public List<Dictionary<string, MealAttendanceDto>> Days { get; set; }
}
}