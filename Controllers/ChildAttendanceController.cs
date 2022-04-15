using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Rollcall.Specifications;
using Rollcall.Models;
using Rollcall.Repositories;
using Rollcall.Services;

namespace Rollcall.Controllers
{
    class ChildMealUpdate
    {
        public IDictionary<string, bool> Meals { get; set; }
    }

    [ApiController]
    [Route("attendance/child")]
    public class ChildAttendanceController : ControllerBase
    {
        private readonly ILogger<ChildAttendanceController> _logger;
        private readonly IMealService _mealService;
        public ChildAttendanceController(ILogger<ChildAttendanceController> logger,
        IMealService mealService)
        {
            _logger = logger;
            _mealService = mealService;
        }

        [HttpGet, Authorize]
        [Route("monthly/{childId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<AttendanceCountDto> GetMonthlyCount(int childId, int year, int month)
        {
            return HandleRequest(() => _mealService.GetMonthlySummary(childId, year, month));
        }

        [HttpGet, Authorize]
        [Route("daily/{childId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<IEnumerable<DayAttendanceDto>> GetChildMonthlyAttendance(int childId, int year, int month)
        {
            _logger.LogInformation($"getting monthly attendance of {childId}");
            return HandleRequest(() => _mealService.GetDailySummaries(childId, year, month));
        }

        [HttpGet, Authorize]
        [Route("daily/{childId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<DayAttendanceDto> GetChildAttendance(int childId, int year, int month, int day)
        {
            return HandleRequest(() => _mealService.GetDailySummary(childId, year, month, day));
        }

        [HttpPost, Authorize]
        [Route("{childId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(FutureDateValidationFilter))]
        public async Task<ActionResult<IDictionary<string, bool>>> SetAttendance(int childId, int year, int month, int day, [FromBody] IDictionary<string, bool> update)
        {
            try
            {
                _logger.LogInformation("ChildController: Trying to update child");
                var newAttendance = await _mealService.UpdateAttendance(update, childId, year, month, day);
                return Ok(newAttendance.Meals);
            }
            catch (InvalidDataException e)
            {
                return NotFound();
            }
        }
        private ActionResult<T> HandleRequest<T>(Func<T> del)
        {
            try
            {
                var result = del();
                return Ok(result);
            }
            catch (InvalidDataException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}