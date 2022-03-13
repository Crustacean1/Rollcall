using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class ChildAttendanceService
    {
        private readonly ChildAttendanceRepository _childRepo;
        private readonly ChildMaskRepository _maskRepo;
        private readonly DtoShapingService _dtoShaper;
        private readonly SchemaService _schemaService;
        public ChildAttendanceService(ChildAttendanceRepository childRepo, ChildMaskRepository maskRepo, DtoShapingService dtoShaper, SchemaService schemaService)
        {
            _childRepo = childRepo;
            _maskRepo = maskRepo;
            _dtoShaper = dtoShaper;
            _schemaService = schemaService;
        }
        public DayAttendanceDto GetDailyAttendance(Child child, int year, int month, int day)
        {
            var attendanceData = _childRepo.GetAttendance(child, year, month, day);
            var maskData = _maskRepo.GetMask(child, year, month, day);

            var result = _dtoShaper.CreateDailyAttendance(attendanceData, maskData);

            return result;
        }
        public MonthlyAttendanceDto GetMonthlyAttendance(Child child, int year, int month)
        {
            var attendanceData = _childRepo.GetMonthlyAttendance(child, year, month);
            var maskData = _maskRepo.GetMasks(child, year, month);

            var result = _dtoShaper.CreateMonthlyAttendance(year, month, attendanceData, maskData);
            return result;
        }
        public AttendanceSummaryDto GetMonthlySummary(Child child, int year, int month)
        {
            var attendanceData = _childRepo.GetMonthlySummary(child, year, month);
            var result = _dtoShaper.CreateMonthlySummary(attendanceData);
            return result;
        }
        public async Task<AttendanceRequestDto> SetAttendance(Child child, AttendanceRequestDto dto, int year, int month, int day)
        {
            var result = await _childRepo.SetAttendance(child, _schemaService.Translate(dto.Name), dto.Present, year, month, day);
            return new AttendanceRequestDto{
                Name = dto.Name,
                Present = result
            };
        }
    }
}