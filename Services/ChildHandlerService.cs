using Rollcall.Models;

namespace Rollcall.Services
{
    interface IHandlerService<T>
    {
        public void AddChild(Child child);
        public Child GetChild(int childId);
        public AttendanceSummaryDto GetMonthlySummary(T child, int year, int month);
        public List<Attendance> GetMonthlyAttendance(T child, int year, int month);
        public List<Attendance> GetMonthlyMasks(T child, int year, int month);
        public Attendance GetDailyAttendance(T child, int year, int month, int day);
        public Attendance GetDailyMask(T child, int year, int month, int day);

    }
    public class ChildHandlerService
    {
        private readonly IAttendanceParserService _parserService;
        public ChildHandlerService(IAttendanceParserService parserService)
        {
            _parserService = parserService;
        }

        public ChildDto ToDto(Child child)
        {
            return new ChildDto
            {
                Name = child.Name,
                Id = child.Id,
                Surname = child.Surname,
                GroupId = child.GroupId,
                GroupName = (child.MyGroup == null) ? "" : child.MyGroup.Name,
                DefaultAttendance = _parserService.Parse(child.DefaultMeals)
            };
        }
        public Child FromDto(ChildDto dto)
        {
            return new Child
            {
                Name = dto.Name,
                Surname = dto.Surname,
                GroupId = dto.GroupId,
                DefaultMeals = _parserService.Marshall(dto.DefaultAttendance)
            };
        }
    }
}