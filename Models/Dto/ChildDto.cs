namespace Rollcall.Models{
    public class ChildDto{
        public string? Name{get;set;}
        public string? Surname{get;set;}
        public int GroupId{get;set;}
        public string? GroupName{get;set;}
        public ICollection<MealDto>? DefaultMeals{get;set;}
    }
}