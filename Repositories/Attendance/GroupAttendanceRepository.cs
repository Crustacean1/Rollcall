using Microsoft.EntityFrameworkCore;
using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{
    public class GroupAttendanceRepository : AttendanceRepositoryBase, IAttendanceRepository<Group>
    {
        public GroupAttendanceRepository(RepositoryContext context,
                                        IMealParserService mealParser) : base(context, mealParser) { }

        public IEnumerable<AttendanceDto> GetMonthlyAttendance(Group target, int year, int month)
        {
            var data = GetAttendanceQuery(
            c => (c.Date.Year == year && c.Date.Month == month && c.TargetChild.GroupId == target.Id),
            c => new { c.Date.Day },
            c => new { c.Id },
            c => new MealDate { Year = year, Month = month, Day = c.Day },
            c => c.Id);
            return data.GroupBy(d => new { d.Date, d.ChildId }).Select(d => new AttendanceDto
            {
                Date = d.Key.Date,
                Attendance = d.ToDictionary(m => m.Name, m => new MealDto { Present = m.Attendance, Masked = m.Masked })
            });
        }
        public AttendanceDto? GetMonthlySummary(Group target, int year, int month)
        {
            var data = GetAttendanceQuery(
            c => (c.Date.Year == year && c.Date.Month == month && c.TargetChild.GroupId == target.Id),
            c => new { },
            c => new { },
            c => new MealDate { Year = year, Month = month, Day = 1 },
            c => target.Id);
            return new AttendanceDto
            {
                Date = new MealDate { Year = year, Month = month, Day = 1 },
                Attendance = data.ToDictionary(d => d.Name, d => new MealDto { Present = d.Attendance, Masked = d.Masked })
            };
        }
        public AttendanceDto? GetAttendance(Group target, int year, int month, int day)
        {
            var data = GetAttendanceQuery(
            c => (c.Date.Year == year && c.Date.Month == month && c.Date.Day == day && c.TargetChild.GroupId == target.Id),
            c => new { },
            c => new { },
            c => new MealDate { Year = year, Month = month, Day = 1 },
            c => target.Id);
            return new AttendanceDto
            {
                Date = new MealDate { Year = year, Month = month, Day = 1 },
                Attendance = data.ToDictionary(d => d.Name, d => new MealDto { Present = d.Attendance, Masked = d.Masked })
            };
        }
    }
}