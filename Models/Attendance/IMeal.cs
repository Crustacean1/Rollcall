namespace Rollcall.Models{
    public interface IMeal{
        public string MealName{get;set;}
        public bool Attendance{get;set;}
        public DateTime Date{get;set;}
    }
}