using System.ComponentModel.DataAnnotations.Schema;
namespace Rollcall.Models
{
    public class ChildAttendance
    {
        public Child TargetChild { get; set; }
        public int ChildId { get; set; }
        [ForeignKey("MealId")]
        public MealSchema Schema { get; set; }
        public int MealId { get; set; }
        public bool Attendance { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}