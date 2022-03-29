namespace Rollcall.Models
{
    public class DailyChildAttendanceDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GroupName { get; set; }
        public int ChildId { get; set; }
        public Dictionary<string, MealAttendanceDto> Meals { get; set; }
    }
    public class DailyGroupAttendanceDto
    {
        public Dictionary<string, IEnumerable<DailyChildAttendanceDto>> Present { get; set; }
    }
}