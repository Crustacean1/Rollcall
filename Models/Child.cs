namespace Rollcall.Models
{
    public class Child
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int DefaultMeals{get;set;}
        public int Id{get;set;}
        public int GroupId{get;set;}
        public Group? MyGroup{get;set;}
    }
}