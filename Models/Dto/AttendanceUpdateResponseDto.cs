namespace Rollcall.Models{
    public class AttendanceUpdateResponseDto{
        public int TargetId{get;set;}
        public IEnumerable<AttendanceRequestDto> Update{get;set;}
    }
}