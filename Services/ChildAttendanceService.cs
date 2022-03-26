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
        private readonly ILogger<ChildAttendanceService> _logger;
        public ChildAttendanceService(ChildAttendanceRepository childRepo, ChildMaskRepository maskRepo, DtoShapingService dtoShaper, SchemaService schemaService,
        ILogger<ChildAttendanceService> logger)
        {
            _childRepo = childRepo;
            _maskRepo = maskRepo;
            _dtoShaper = dtoShaper;
            _schemaService = schemaService;
            _logger = logger;
        }
        public DayAttendanceDto GetDailyAttendance(Child child, int year, int month)
        {
            var attendanceData = _childRepo.GetAttendance(child, year, month);
            var maskData = _maskRepo.GetMasks(child, year, month);

            var result = _dtoShaper.CreateDailyAttendance(attendanceData, maskData);

            return result;
        }
        public MonthlyAttendanceDto GetMonthlyAttendance(Child child, int year, int month)
        {
            var attendanceData = _childRepo.GetAttendance(child, year, month);
            var maskData = _maskRepo.GetMasks(child, year, month);

            var result = _dtoShaper.CreateMonthlyAttendance(year, month, attendanceData, maskData);
            return result;
        }
        public AttendanceCountDto GetMonthlyCount(Child child, int year, int month)
        {
            var attendanceData = _childRepo.GetMonthlyCount(child, year, month);
            var result = _dtoShaper.CreateMonthlyCount(attendanceData.ToList());
            return result;
        }
        public async Task<List<AttendanceRequestDto>> SetAttendance(Child child, List<AttendanceRequestDto> dto, int year, int month, int day)
        {
            var updatedAttendance = new List<AttendanceRequestDto>();
            foreach (var meal in dto)
            {
                _logger.LogInformation("Updating child");
                var mealResult = _childRepo.SetAttendance(child, _schemaService.Translate(meal.Name), meal.Present, year, month, day);
                updatedAttendance.Add(new AttendanceRequestDto
                {
                    Name = meal.Name,
                    Present = mealResult
                });
            }
            await _childRepo.SaveChangesAsync();
            return updatedAttendance;
        }
        public async Task<int> ExtendAttendance(IEnumerable<Child> children,int year,int month){
            var updatedAttendances = _childRepo.ExtendAttendance(children,year,month);
            await _childRepo.SaveChangesAsync();
            return updatedAttendances;
        }
    }
}