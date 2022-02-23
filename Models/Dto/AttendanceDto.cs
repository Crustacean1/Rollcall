namespace Rollcall.Models{
    public class MealDto{
        public string? Name{get;set;}
        public bool Present{get;set;}
    }
    public class AttendanceDto{
        public ICollection<MealDto>? Meals{get;set;}
        public int ChildId{get;set;}
        public int Year{get;set;}
        public int Month{get;set;}
        public int Day{get;set;}
    }
}