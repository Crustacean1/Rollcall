using Rollcall.Models;

namespace Rollcall.Services
{
    public interface IAttendanceParserService
    {
        public AttendanceSummary MarshallSummary(Attendance attendance);
        public AttendanceData Marshall(Attendance attendance);
        public AttendanceData Marshall(Mask mask);
        public Attendance Parse(AttendanceData dto);
    }
}