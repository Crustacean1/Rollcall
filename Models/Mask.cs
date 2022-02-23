namespace Rollcall.Models
{
    public class Mask
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public Group? MaskedGroup { get; set; }
        public int Meals { get; set; }
        public DateTime Date { get; set; }
    }
}