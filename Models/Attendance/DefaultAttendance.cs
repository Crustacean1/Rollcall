namespace Rollcall.Models
{
    public class DefaultAttendance
    {
        public MealSchema Schema { get; set; }
        public int MealId { get; set; }
        public Child TargetChild { get; set; }
        public int ChildId { get; set; }
        public bool Attendance { get; set; }
    }
}