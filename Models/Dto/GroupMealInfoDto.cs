namespace Rollcall.Models
{
    public class GroupMealInfoDto
    {
        public IDictionary<string, bool> Masks { get; set; }
        public IEnumerable<MealInfoDto> Children { get; set; }
    }
}