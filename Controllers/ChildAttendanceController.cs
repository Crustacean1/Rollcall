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
        private readonly IMealService<Child> _mealService;
        public ChildAttendanceController(ILogger<ChildAttendanceController> logger,
        IMealService<Child> mealService)
        {
            _logger = logger;
            _mealService = mealService;
        }

        [HttpGet, Authorize]
        [Route("monthly/{childId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        [ServiceFilter(typeof(ChildExtractorFilter))]
        public ActionResult<IDictionary<string, int>> GetMonthlyCount(int childId, int year, int month)
        {
            return _mealService.GetMonthlySummary((Child)HttpContext.Items["child"], year, month);
        }

        [HttpGet, Authorize]
        [Route("daily/{childId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        [ServiceFilter(typeof(ChildExtractorFilter))]
        public ActionResult<IEnumerable<IDictionary<string, MealAttendanceDto>>> GetChildMonthlyAttendance(int childId, int year, int month)
        {
            _logger.LogInformation($"getting monthly attendance of {childId}");
            return _mealService.GetDailySummaries((Child)HttpContext.Items["child"], year, month).ToList();
        }

        [HttpGet, Authorize]
        [Route("daily/{childId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        [ServiceFilter(typeof(ChildExtractorFilter))]
        public ActionResult<IDictionary<string,MealAttendanceDto>> GetChildAttendance(int childId, int year, int month, int day)
        {
            return _mealService.GetDailySummary((Child)HttpContext.Items["child"], year, month, day);
        }

        [HttpPost, Authorize]
        [Route("{childId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(MealUpdateDateFilter))]
        [ServiceFilter(typeof(ChildExtractorFilter))]
        public async Task<ActionResult<IDictionary<string, bool>>> SetAttendance([FromBody] IDictionary<string, bool> update, int childId, int year, int month, int day)
        {
            _logger.LogInformation("ChildController: Trying to update child");
            var newAttendance = await _mealService.UpdateAttendance(update, (Child)HttpContext.Items["child"], year, month, day);
            return Ok(newAttendance.Meals);
        }
    }
}