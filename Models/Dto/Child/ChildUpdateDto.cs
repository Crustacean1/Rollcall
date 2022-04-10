namespace Rollcall.Models
{
    public class ChildUpdateDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int GroupId { get; set; }
        public IDictionary<string, bool>? DefaultMeals { get; set; }
    }
}