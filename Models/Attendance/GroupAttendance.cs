using System.ComponentModel.DataAnnotations.Schema;

namespace Rollcall.Models
{
    public class GroupAttendance
    {

        public Group TargetGroup { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("MealId")]
        public MealSchema Schema { get; set; }
        public int MealId { get; set; }
        public bool Attendance { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}