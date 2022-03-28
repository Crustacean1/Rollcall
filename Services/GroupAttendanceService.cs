using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class GroupAttendanceService
    {
        private readonly GroupAttendanceRepository _groupRepo;
        private readonly AttendanceSummaryRepository _summaryRepo;
        private readonly GroupMaskRepository _maskRepo;
        private readonly DtoShapingService _dtoShaper;
        private readonly SchemaService _schemaService;
        private readonly ILogger<GroupAttendanceService> _logger;
        public GroupAttendanceService(GroupAttendanceRepository groupRepo,
        AttendanceSummaryRepository summaryRepo,
        GroupMaskRepository maskRepo,
        DtoShapingService dtoShaper,
        SchemaService schemaService,
        ILogger<GroupAttendanceService> logger)
        {
            _maskRepo = maskRepo;
            _summaryRepo = summaryRepo;
            _groupRepo = groupRepo;
            _dtoShaper = dtoShaper;
            _schemaService = schemaService;
            _logger = logger;
        }
        public MonthlyAttendanceDto GetMonthlyAttendance(Group? target, int year, int month)
        {
            var attendanceData = _groupRepo.GetAttendance(target, target == null, year, month);
            var maskData = _maskRepo.GetMasks(target, year, month);
            var result = _dtoShaper.CreateMonthlyAttendance(year, month, attendanceData, maskData);
            return result;
        }
        public DayAttendanceDto GetDailyCount(Group? target, int year, int month, int day)
        {
            var attendanceData = _groupRepo.GetSummary(target, target == null, year, month, day);
            var maskData = _maskRepo.GetMasks(target, year, month, day);
            var result = _dtoShaper.CreateDailyAttendance(attendanceData, maskData);
            return result;
        }
        public AttendanceCountDto GetMonthlyCount(Group? target, int year, int month)
        {
            var attendanceData = _groupRepo.GetSummary(target, true, year, month);
            var result = _dtoShaper.CreateMonthlyCount(attendanceData);
            return result;
        }
        public IEnumerable<ChildAttendanceSummaryDto> GetMonthlySummary(int year, int month)
        {
            var attendanceData = _summaryRepo.GetMonthlySummary(year, month);
            var result = _dtoShaper.CreateMonthlySummary(attendanceData);
            return result;
        }
        public IEnumerable<ChildAttendanceSummaryDto> GetDailySummary(Group group, int year, int month, int day)
        {
            var attendanceData = _summaryRepo.GetDailyList(group, new MealDate(year, month, day));
            var result = _dtoShaper.CreateMonthlySummary(attendanceData);
            return result;
        }
        public async Task<List<AttendanceRequestDto>> SetAttendance(Group? target, List<AttendanceRequestDto> dto, MealDate date)
        {
            var result = new List<AttendanceRequestDto>();
            foreach (var meal in dto)
            {
                var present = _groupRepo.SetGroupAttendance(target, _schemaService.Translate(meal.Name), meal.Present, date);
                result.Add(new AttendanceRequestDto
                {
                    Name = meal.Name,
                    Present = present
                });
            }
            await _groupRepo.SaveChangesAsync();
            return result;
        }
    }
}