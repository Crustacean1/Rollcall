using System.ComponentModel.DataAnnotations.Schema;
namespace Rollcall.Models
{
    public class Attendance
    {
        public int ChildId { get; set; }
        public Child TargetChild { get; set; }
        public uint Meals { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}