using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class AttendanceHandlerService
    {
        private readonly ILogger<AttendanceHandlerService> _logger;
        private readonly AttendanceRepository _attendanceRepo;
        private readonly IChildRepository _childRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly MaskRepository _maskRepo;
        private readonly Dictionary<string, int> _schema;
        private readonly IMealParserService _mealParser;
        public AttendanceHandlerService(ILogger<AttendanceHandlerService> logger,
        AttendanceRepository attendanceRepo,
        IChildRepository childRepo,
        IGroupRepository groupRepo,
        MaskRepository maskRepo,
        IMealParserService mealParser,
        SchemaService schemaService)
        {
            _logger = logger;
            _attendanceRepo = attendanceRepo;
            _childRepo = childRepo;
            _groupRepo = groupRepo;
            _maskRepo = maskRepo;
            _mealParser = mealParser;
            _schema = schemaService.GetSchemas();
        }
        public List<ChildAttendanceDto> GetChildAttendance(int childId, int year, int month, int day)
        {
            var attendance = _attendanceRepo.GetChildAttendance(childId, year, month, day);
            return attendance.Select(a => new ChildAttendanceDto
            {
                Year = a.Date.Year,
                Month = a.Date.Month,
                Day = a.Date.Day,
                Meals = _mealParser.ToDto(a.Meals, _schema)
            }).ToList();
        }
        public List<GroupAttendanceDto>? GetGroupAttendance(int groupId, int year, int month, int day)
        {
            _logger.LogInformation("Starting fetching data");
            var result = _attendanceRepo.GetGroupAttendance(groupId, year, month);
            foreach (var entry in result)
            {
                _logger.LogInformation($"Entry: {entry.Day}/{entry.Month}/{entry.Year} MealId: {entry.MealName} : {entry.MealCount}");
            }
            _logger.LogInformation("Done");
        }
        /**
        * @name SetChildAttendance
        * @throws ArgumentOutOfRangeException InvalidDataException
        */
        public async Task SetChildAttendance(int childId, int year, int month, int day, Dictionary<string, bool> meals)
        {
            if (day == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (_childRepo.GetChild(childId) == null)
            {
                throw new InvalidDataException();
            }
            var attendance = new Attendance
            {
                ChildId = childId,
                Meals = _mealParser.FromDto(meals, _schema),
                Date = new DateTime(year, month, day)
            };
            _attendanceRepo.SetAttendance(attendance);
            await _attendanceRepo.SaveChangesAsync();
        }

        /**
        * @name SetGroupAttendanceMask
        * @throws ArgumentOutOfRangeException InvalidDataException
        */
        public async Task SetGroupAttendanceMask(int groupId, int year, int month, int day, Dictionary<string, bool> meals)
        {
            if (day == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (_groupRepo.GetGroup(groupId) == null)
            {
                throw new InvalidDataException();
            }
            _maskRepo.SetMask(
                new Mask
                {
                    GroupId = groupId,
                    Date = new DateTime(year, month, day),
                    Meals = _mealParser.FromDto(meals, _schema)
                }
            );
            await _maskRepo.SaveChangesAsync();
        }
    }
}