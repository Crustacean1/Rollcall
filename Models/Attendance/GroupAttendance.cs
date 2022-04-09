using System.ComponentModel.DataAnnotations.Schema;

namespace Rollcall.Models
{
    public class GroupAttendance
    {

        public Group TargetGroup { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("MealName")]
        public MealSchema Schema { get; set; }
        public string MealName { get; set; }
        public bool Attendance { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}