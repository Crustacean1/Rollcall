using System.ComponentModel.DataAnnotations.Schema;
namespace Rollcall.Models
{
    public class DefaultMeal
    {
        [ForeignKey("MealName")]
        public MealSchema Schema { get; set; }
        public string MealName { get; set; }
        [ForeignKey("ChildId")]
        public Child TargetChild { get; set; }
        public int ChildId { get; set; }
        public bool Attendance { get; set; }
    }
}