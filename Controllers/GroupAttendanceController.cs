
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly GroupRepository _groupRepo;
        private readonly GroupAttendanceService _attendanceService;
        public GroupAttendanceController(ILogger<GroupAttendanceController> logger,
        GroupAttendanceService attendanceService,
        GroupRepository groupRepo)
        {
            _logger = logger;
            _attendanceService = attendanceService;
            _groupRepo = groupRepo;
        }

        [HttpGet, Authorize]
        [Route("count/{groupId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<AttendanceCountDto> GetMonthlyCount(int groupId, int year, int month)
        {
            Group? group = null;
            if (groupId != 0)
            {
                group = _groupRepo.GetGroup(groupId);
                if (group == null)
                {
                    return NotFound();
                }
            }
            var result = _attendanceService.GetMonthlyCount(group, year, month);
            return Ok(result);
        }

        [HttpGet, Authorize]
        [Route("summary/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<IEnumerable<ChildAttendanceSummaryDto>> GetMonthlySummary(int year, int month)
        {
            var result = _attendanceService.GetMonthlySummary(year, month);
            return result.ToList();
        }
        [HttpGet, Authorize]
        [Route("summary/{groupId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<IEnumerable<ChildAttendanceSummaryDto>> GetDailySummary(int groupId, int year, int month, int day)
        {
            var group = _groupRepo.GetGroup(groupId);
            if (group == null) { return NotFound(); }
            var result = _attendanceService.GetDailySummary(group, year, month, day);
            return result.ToList();
        }

        [HttpGet, Authorize]
        [Route("daily/{groupId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<List<DayAttendanceDto>> GetMonthlyAttendance(int groupId, int year, int month)
        {
            Group? group = null;
            if (groupId != 0)
            {
                group = _groupRepo.GetGroup(groupId);
                if (group == null)
                {
                    return NotFound();
                }
            }
            _logger.LogInformation($"Is group null: {group == null}");
            var result = _attendanceService.GetMonthlyAttendance(group, year, month);
            return Ok(result);
        }

        [HttpGet, Authorize]
        [Route("daily/{groupId}/{year}/{month}/{day}")]
        public ActionResult<DayAttendanceDto> GetDailyCount(int groupId, int year, int month, int day)
        {
            Group? group = null;
            if (groupId != 0)
            {
                group = _groupRepo.GetGroup(groupId);
                if (group == null)
                {
                    return NotFound();
                }
            }
            return Ok(_attendanceService.GetDailyCount(group, year, month, day));
        }

        [HttpPost, Authorize]
        [Route("{groupId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(FutureDateValidationFilter))]
        public async Task<ActionResult<AttendanceRequestDto>> SetAttendance(int groupId, int year, int month, int day, [FromBody] List<AttendanceRequestDto> dto)
        {
            Group? group = null;
            if (groupId != 0)
            {
                group = _groupRepo.GetGroup(groupId);
                if (group == null)
                {
                    return NotFound();
                }
            }

            var date = new MealDate(year, month, day);
            var result = await _attendanceService.SetAttendance(group, dto, date);
            return Ok(result);
        }
    }
}