namespace Rollcall.Models
{
    public class ChildAttendanceSummaryDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ChildId { get; set; }
        public Dictionary<string,int> Summary { get; set; }
    };
}