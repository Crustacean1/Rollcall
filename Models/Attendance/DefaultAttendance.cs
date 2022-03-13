using System.ComponentModel.DataAnnotations.Schema;
namespace Rollcall.Models
{
    public class DefaultAttendance
    {
        [ForeignKey("MealId")]
        public MealSchema Schema { get; set; }
        public int MealId { get; set; }
        [ForeignKey("ChildId")]
        public Child TargetChild { get; set; }
        public int ChildId { get; set; }
        public bool Attendance { get; set; }
    }
}