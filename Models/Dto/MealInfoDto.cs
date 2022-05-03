namespace Rollcall.Models
{
    public class MealInfoDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ChildId { get; set; }
        public string GroupName { get; set; }
        public Dictionary<string, int> Summary { get; set; }
    };
}