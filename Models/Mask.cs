using System.ComponentModel.DataAnnotations.Schema;

namespace Rollcall.Models
{
    public class Mask : ICloneable
    {
        public Group MaskedGroup { get; set; }
        public int Meals { get; set; }
        public int GroupId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public object Clone(){
            return new Mask{
                MaskedGroup = MaskedGroup,
                Meals = Meals,
                GroupId = GroupId,
                Date = Date
            };
        }
    }
}