namespace Rollcall.Models
{
    public class DailyChildAttendanceDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ChildId{get;set;}
        public Dictionary<string, bool> meals{get;set;}
    }
    public class DailyGroupAttendanceDto
    {
        public IEnumerable<MaskDto> masks{get;set;}
        public Dictionary<string, IEnumerable<DailyChildAttendanceDto>> present{get;set;}
    }
}