namespace Rollcall.Models{
    public class Attendance{
        public int ChildId{get;set;}
        public Child? TargetChild{get;set;}
        public int Meals{get;set;} 
        public DateTime Date{get;set;}
    }
}