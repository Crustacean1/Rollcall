
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
        [Route("summary/{groupId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<AttendanceSummaryDto> GetMonthlySummary(int groupId, int year, int month)
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
            var result = _attendanceService.GetMonthlySummary(group, year, month);
            return Ok(result);
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
        public ActionResult<DayAttendanceDto> GetDailySummary(int groupId, int year, int month, int day)
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
            return Ok(_attendanceService.GetDailySummary(group, year, month, day));
        }

        [HttpPost, Authorize]
        [Route("{groupId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public async Task<ActionResult<AttendanceRequestDto>> SetAttendance(int groupId, int year, int month, int day, [FromBody] List<AttendanceRequestDto> dto)
        {
            var group = _groupRepo.GetGroup(groupId);
            if (group == null)
            {
                return NotFound();
            }
            var result = await _attendanceService.SetAttendance(group, dto, year, month, day);
            return Ok(result);
        }
    }
}