using System.ComponentModel.DataAnnotations.Schema;
namespace Rollcall.Models
{
    public class ChildAttendance
    {
        public Child TargetChild { get; set; }
        public int ChildId { get; set; }
        [ForeignKey("MealName")]
        public MealSchema Schema { get; set; }
        public string MealName { get; set; }
        public bool Attendance { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}