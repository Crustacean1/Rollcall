using Rollcall.Repositories;
using Rollcall.Models;

namespace Rollcall.Services
{
    public class MaskHandlerService
    {
        private readonly MaskRepository _maskRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly ILogger<MaskHandlerService> _logger;
        private readonly IAttendanceParserService _mealParser;
        public MaskHandlerService(MaskRepository maskRepo,
        IGroupRepository groupRepo,
        IAttendanceParserService mealParser,
        ILogger<MaskHandlerService> logger)
        {
            _maskRepo = maskRepo;
            _groupRepo = groupRepo;
            _mealParser = mealParser;
            _logger = logger;
        }
        public async Task<Dictionary<string, bool>> SetMask(int groupId, int year, int month, int day, Dictionary<string, bool> mealMask)
        {
            var mask = _maskRepo.GetMask(groupId, year, month, day, true);

            if (mask != null)
            {
                mask.Meals = _mealParser.ChangeAttendance(mask.Meals, mealMask);
            }
            else
            {
                mask = new Mask
                {
                    Meals = _mealParser.ChangeAttendance(_mealParser.GetFullAttendance(), mealMask),
                    GroupId = groupId,
                    Date = new DateTime(year, month, day)
                };
                _maskRepo.AddMask(mask);
            }
            await _maskRepo.SaveChangesAsync();
            return _mealParser.Parse(mask.Meals);
        }
        public List<AttendanceDto> GetMasks(int groupId, int year, int month, int day)
        {
            return _maskRepo.GetMasks(groupId, year, month, day).Select(m =>
            {
                return new AttendanceDto
                {
                    Date = new MealDate
                    {
                        Year = m.Date.Year,
                        Month = m.Date.Month,
                        Day = m.Date.Day
                    },
                    Meals = _mealParser.Parse(m.Meals)
                };
            }).ToList();
        }
        public async Task ProlongMasks(int groupId, int year, int month)
        {
            var prolongedMasks = new List<Mask>();

            for (var date = new DateTime(year, month, 1); date.Month == month; date.AddDays(1))
            {
                uint meals = _mealParser.GetFullAttendance();
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                {
                    meals = _mealParser.GetEmptyAttendance();
                }
                prolongedMasks.Add(new Mask
                {
                    Meals = meals,
                    Date = date,
                    GroupId = groupId
                });
            }
            _maskRepo.AddMasks(prolongedMasks);
            await _maskRepo.SaveChangesAsync();
        }
    }
}