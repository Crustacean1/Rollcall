namespace Rollcall.Models
{
    public class GroupMealInfoDto
    {
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public IDictionary<string, bool> Masks { get; set; }
        public IEnumerable<MealInfoDto> Children { get; set; }
    }
}