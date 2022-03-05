using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{
    public class GroupAttendanceRepository : AttendanceRepositoryBase, IAttendanceRepository<Group>
    {
        public SchemaService _schemaService;
        public GroupAttendanceRepository(RepositoryContext context, SchemaService schemaService) : base(context)
        {
            _schemaService = schemaService;
        }

        public IEnumerable<AttendanceDto> GetMonthlyAttendance(Group target, int year, int month)
        {
            /*var data = GetAttendanceQuery(
            c => (c.Date.Year == year && c.Date.Month == month && c.TargetChild.GroupId == target.Id),
            c => new { c.Date.Day },
            c => new { c.Id },
            c => new MealDate { Year = year, Month = month, Day = c.Day },
            c => c.Id);
            return data.GroupBy(d => new { d.Date, d.ChildId }).Select(d => new AttendanceDto
            {
                Date = d.Key.Date,
                Attendance = d.ToDictionary(m => m.Name, m => new MealDto { Present = m.Attendance, Masked = m.Masked })
            });*/
            return new List<AttendanceDto>();
        }
        public AttendanceDto? GetMonthlySummary(Group target, int year, int month)
        {
            /*var data = GetAttendanceQuery(
            c => (c.Date.Year == year && c.Date.Month == month && c.TargetChild.GroupId == target.Id),
            c => new { },
            c => new { },
            c => new MealDate { Year = year, Month = month, Day = 1 },
            c => target.Id);
            return new AttendanceDto
            {
                Date = new MealDate { Year = year, Month = month, Day = 1 },
                Attendance = data.ToDictionary(d => d.Name, d => new MealDto { Present = d.Attendance, Masked = d.Masked })
            };*/
            return new AttendanceDto();
        }
        public AttendanceDto? GetAttendance(Group target, int year, int month, int day)
        {
            /*var data = GetAttendanceQuery(
            c => (c.Date.Year == year && c.Date.Month == month && c.Date.Day == day && c.TargetChild.GroupId == target.Id),
            c => new { },
            c => new { },
            c => new MealDate { Year = year, Month = month, Day = 1 },
            c => target.Id);
            return new AttendanceDto
            {
                Date = new MealDate { Year = year, Month = month, Day = 1 },
                Attendance = data.ToDictionary(d => d.Name, d => new MealDto { Present = d.Attendance, Masked = d.Masked })
            };*/
            return new AttendanceDto();
        }
        public async Task SetAttendance(Group target, AttendanceRequestDto attendance, int year, int month, int day)
        {
            _context.GroupAttendance.Add(new GroupAttendance
            {
                GroupId = target.Id,
                Date = new DateTime(year, month, day),
                Attendance = attendance.Present,
                MealId = _schemaService.Translate(attendance.Name)
            });

            await _context.SaveChangesAsync();
        }
    }
}