
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Rollcall.Specifications;
using Rollcall.Models;
using Rollcall.Repositories;
using Rollcall.Services;

namespace Rollcall.Controllers
{
    [ApiController]
    [Route("attendance/group")]
    public class GroupAttendanceController : ControllerBase
    {
        private readonly ILogger<GroupAttendanceController> _logger;
        private readonly IGroupMealService _groupService;
        public GroupAttendanceController(ILogger<GroupAttendanceController> logger,
        IGroupMealService groupService,
        GroupRepository groupRepo)
        {
            _logger = logger;
            _groupService = groupService;
        }

        [HttpGet, Authorize]
        [Route("monthly/{groupId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<AttendanceCountDto> GetMonthlyCount(int groupId, int year, int month)
        {
            try
            {
                var result = _groupService.GetMonthlySummary(groupId, year, month);
                return Ok(result);
            }
            catch (InvalidDataException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("childlist/{groupId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<IEnumerable<DailyChildAttendanceDto>> GetDailySummary(int groupId, int year, int month, int day)
        {
            try
            {
                var result = _groupService.GetDailyInfo(groupId, year, month, day);
                return Ok(result);
            }
            catch (InvalidDataException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("daily/{groupId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<IEnumerable<DayAttendanceDto>> GetMonthlyAttendance(int groupId, int year, int month)
        {
            try
            {
                var result = _groupService.GetDailySummaries(groupId, year, month);
                return Ok(result);
            }
            catch (InvalidDataException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("daily/{groupId}/{year}/{month}/{day}")]
        public ActionResult<DayAttendanceDto> GetDailyCount(int groupId, int year, int month, int day)
        {
            try
            {
                var result = _groupService.GetDailySummary(groupId, year, month, day);
                return Ok(result);
            }
            catch (InvalidDataException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("{groupId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(FutureDateValidationFilter))]
        public async Task<ActionResult<AttendanceRequestDto>> SetAttendance(int groupId, int year, int month, int day, [FromBody] IDictionary<string, bool> update)
        {
            try
            {
                var result = await _groupService.UpdateAttendance(update, groupId, year, month, day);
                return Ok(result);
            }
            catch (InvalidDataException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}