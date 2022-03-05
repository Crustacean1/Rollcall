using Microsoft.EntityFrameworkCore;

using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Repositories
{

    public class ChildAttendanceRepository : AttendanceRepositoryBase, IAttendanceRepository<Child>
    {
        private readonly IAttendanceParserService _attendanceParser;
        private readonly IMealParserService _mealParser;
        public ChildAttendanceRepository(RepositoryContext context,
        IMealParserService mealParser,
        IAttendanceParserService attendanceParser) : base(context, mealParser)
        {
            _attendanceParser = attendanceParser;
            _mealParser = mealParser;
        }
        public IEnumerable<AttendanceDto> GetMonthlyAttendance(Child target, int year, int month)
        {
            var entries = GetAttendanceQuery(
                c => (c.Date.Year == year && c.Date.Month == month && c.ChildId == target.Id),
                c => new { c.Date.Day },
                c => new { },
                c => new MealDate { Year = year, Month = month, Day = c.Day },
                c => target.Id
            );
            var data = entries.GroupBy(e => e.Date.Day).Select(
                d => new AttendanceDto
                {
                    Attendance = d.ToDictionary(d => d.Name, d => new MealDto { Present = d.Attendance, Masked = d.Masked }),
                    Date = new MealDate { Year = year, Month = month, Day = d.Key }
                }
            );
            return data;
        }
        public AttendanceDto? GetMonthlySummary(Child target, int year, int month)
        {
            var data = GetAttendanceQuery(
                c => (c.Date.Year == year && c.Date.Month == month && c.ChildId == target.Id),
                c => new { },
                c => new { },
                c => new MealDate { Year = year, Month = month, Day = 1 },
                c => target.Id
            ).ToDictionary(e => e.Name, e => new MealDto { Present = e.Attendance, Masked = e.Masked });
            return new AttendanceDto
            {
                Date = new MealDate { Year = year, Month = month, Day = 1 },
                Attendance = data
            };
        }
        public AttendanceDto? GetAttendance(Child target, int year, int month, int day)
        {
            var data = GetAttendanceQuery(
                c => (c.Date.Year == year && c.Date.Month == month && c.Date.Day == day && c.ChildId == target.Id),
                c => new { },
                c => new { },
                c => new MealDate { Year = year, Month = month, Day = day },
                c => target.Id
            ).ToDictionary(e => e.Name, e => new MealDto { Present = e.Attendance, Masked = e.Masked });
            return new AttendanceDto
            {
                Date = new MealDate { Year = year, Month = month, Day = day },
                Attendance = data
            };
        }
    }
}