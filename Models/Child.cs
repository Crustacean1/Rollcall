using System.ComponentModel.DataAnnotations.Schema;

namespace Rollcall.Models
{
    public class Child
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public IEnumerable<DefaultAttendance> DefaultMeals { get; set; }
        public IEnumerable<ChildAttendance> MyAttendance { get; set; }

        public int Id { get; set; }

        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group? MyGroup { get; set; }
    }
}