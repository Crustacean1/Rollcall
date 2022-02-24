namespace Rollcall.Models{
    public class ChildAttendanceDto{
        public Dictionary<string,bool>? Meals{get;set;}
        public int Year{get;set;}
        public int Month{get;set;}
        public int Day{get;set;}
    }
    public class GroupAttendanceDto{
        public Dictionary<string,uint>? Meals{get;set;}
        public Dictionary<string,bool>? MealMasks{get;set;}
        public int Year{get;set;}
        public int Month{get;set;}
        public int Day{get;set;}
    }
}