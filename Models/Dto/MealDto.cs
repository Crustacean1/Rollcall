namespace Rollcall.Models
{
    public class MealAttendanceDto
    {
        public int Present { get; set; }
        public bool Masked { get; set; }
        public MealAttendanceDto()
        {
            Present = 0;
            Masked = false;
        }
    }
}