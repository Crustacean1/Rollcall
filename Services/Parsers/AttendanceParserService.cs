using Rollcall.Models;
namespace Rollcall.Services
{
    public class AttendanceParserService : IAttendanceParserService
    {
        private readonly IMealParserService _mealParser;
        public AttendanceParserService(IMealParserService mealParser)
        {
            _mealParser = mealParser;
        }
        public AttendanceData Marshall(Attendance attendance)
        {
            return new AttendanceData
            {
                Meals = _mealParser.MarshallMask(attendance.Meals),
                Date = new MealDate
                {
                    Year = attendance.Date.Year,
                    Month = attendance.Date.Month,
                    Day = attendance.Date.Day
                }
            };
        }
        public AttendanceData Marshall(Mask attendance)
        {
            return new AttendanceData
            {
                Meals = _mealParser.MarshallMask(attendance.Meals),
                Date = new MealDate
                {
                    Year = attendance.Date.Year,
                    Month = attendance.Date.Month,
                    Day = attendance.Date.Day
                }
            };
        }
        public Attendance Parse(AttendanceData dto)
        {
            return new Attendance
            {
                Meals = _mealParser.Parse(dto.Meals),
                Date = new DateTime(dto.Date.Year, dto.Date.Month, dto.Date.Day)
            };
        }

        public AttendanceSummary MarshallSummary(Attendance attendance)
        {
            var dict = _mealParser.MarshallMeal(attendance.Meals);
            var result = new AttendanceSummary
            {
                Meals = dict,
                Date = new MealDate { Year = attendance.Date.Year, Month = attendance.Date.Month, Day = attendance.Date.Day }
            };
            return result;
        }
    }
}