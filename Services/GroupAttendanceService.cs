using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class GroupAttendanceService
    {
        private readonly GroupAttendanceRepository _groupRepo;
        private readonly GroupMaskRepository _maskRepo;
        private readonly DtoShapingService _dtoShaper;
        private readonly SchemaService _schemaService;
        private readonly ILogger<GroupAttendanceService> _logger;
        public GroupAttendanceService(GroupAttendanceRepository groupRepo,
        GroupMaskRepository maskRepo,
        DtoShapingService dtoShaper,
        SchemaService schemaService,
        ILogger<GroupAttendanceService> logger)
        {
            _maskRepo = maskRepo;
            _groupRepo = groupRepo;
            _dtoShaper = dtoShaper;
            _schemaService = schemaService;
            _logger = logger;
        }
        //public DayAttendanceDto GetDailyAttendance(Group target, int year, int month, int day)
        //{

        //}
        public MonthlyAttendanceDto GetMonthlyAttendance(Group target, int year, int month)
        {
            var attendanceData = _groupRepo.GetMonthlyAttendance(target, year, month);
            var maskData = _maskRepo.GetMasks(target, year, month);
            _logger.LogInformation("attendance size: " + attendanceData.ToList().Count);
            _logger.LogInformation("mask size: " + maskData.ToList().Count);
            var result = _dtoShaper.CreateMonthlyAttendance(year, month, attendanceData, maskData);
            return result;
        }
        public DayAttendanceDto GetDailySummary(Group target, int year, int month, int day)
        {
            var attendanceData = _groupRepo.GetDailySummary(target, year, month, day);
            var maskData = _maskRepo.GetMask(target, year, month, day);
            var result = _dtoShaper.CreateDailyAttendance(attendanceData, maskData);
            return result;
        }
        public AttendanceSummaryDto GetMonthlySummary(Group target, int year, int month)
        {
            var attendanceData = _groupRepo.GetMonthlySummary(target, year, month);
            var result = _dtoShaper.CreateMonthlySummary(attendanceData);
            return result;
        }
        public async Task<List<AttendanceRequestDto>> SetAttendance(Group target, List<AttendanceRequestDto> dto, int year, int month, int day)
        {
            var result = new List<AttendanceRequestDto>();
            foreach (var meal in dto)
            {
                var present = _groupRepo.SetAttendance(target, _schemaService.Translate(meal.Name), meal.Present, year, month, day);
                result.Add(new AttendanceRequestDto
                {
                    Name = meal.Name,
                    Present = present
                });
            }
            await _groupRepo.SaveChangesAsync();
            return result;
        }
        public async Task<bool> ExtendGroupAttendance(Group? target, int year,int month){
            _child
        }
    }
}