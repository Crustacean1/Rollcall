using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services{
    
    public interface IGroupHandlerService{
        public void AddGroup(Group group);
        public Group GetGroup(int groupId);
        public AttendanceSummaryDto GetMonthlySummary(Group group,int year,int month);
        public List<AttendanceSummaryDto> GetMonthlyAttendance(Group group,int year,int month);
        public List<AttendanceDto> GetMonthlyMasks(Group group,int year,int month);
    
    }
    public class GroupHandlerService{

    }
}