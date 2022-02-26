using Rollcall.Repositories;
using Rollcall.Models;

namespace Rollcall.Services
{
    public class MaskHandlerService
    {
        private readonly MaskRepository _maskRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly ILogger<MaskHandlerService> _logger;
        private readonly IMealParserService _mealParser;
        public MaskHandlerService(MaskRepository maskRepo,
        IGroupRepository groupRepo,
        IMealParserService mealParser,
        ILogger<MaskHandlerService> logger)
        {
            _maskRepo = maskRepo;
            _groupRepo = groupRepo;
            _mealParser = mealParser;
            _logger = logger;
        }
        public async Task SetMask(int groupId, int year, int month, int day, Dictionary<string, bool> mealMask)
        {
            _maskRepo.SetMask(new Mask
            {
                Meals = _mealParser.FromDict(mealMask),
                GroupId = groupId,
                Date = new DateTime(year, month, day)
            });
            await _maskRepo.SaveChangesAsync();
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
                    Meals = _mealParser.ToDict(m.Meals)
                };
            }).ToList();
        }
    }
}