namespace Rollcall.Models
{
    public class ChildCreationDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int GroupId { get; set; }
        public IDictionary<string, bool>? DefaultMeals { get; set; }
    }
}