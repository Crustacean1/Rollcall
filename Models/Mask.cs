using System.ComponentModel.DataAnnotations.Schema;

namespace Rollcall.Models
{
    public class Mask
    {
        public Group MaskedGroup { get; set; }
        public uint Meals { get; set; }
        public int GroupId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}