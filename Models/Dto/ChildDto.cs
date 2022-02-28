namespace Rollcall.Models{
    public class ChildDto{
        public int Id{get;set;}
        public string? Name{get;set;}
        public string? Surname{get;set;}
        public int GroupId{get;set;}
        public string? GroupName{get;set;}
        public Dictionary<string,bool>? DefaultAttendance{get;set;}
    }
}