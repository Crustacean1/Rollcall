
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
        private readonly AttendanceService<Group> _attendanceService;
        public GroupAttendanceController(ILogger<GroupAttendanceController> logger,
        AttendanceService<Group> attendanceService,
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
            var child = _groupRepo.GetGroup(groupId);
            if (child == null)
            {
                return NotFound();
            }
            var result = _attendanceService.GetMonthlySummary(child, year, month);
            return Ok(result);
        }

        [HttpGet, Authorize]
        [Route("daily/{groupId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<List<DayAttendanceDto>> GetChildMonthlyAttendance(int groupId, int year, int month)
        {
            var child = _groupRepo.GetGroup(groupId);
            if (child == null)
            {
                return NotFound();
            }
            var result = _attendanceService.GetMonthlyAttendance(child, year, month);
            return Ok(result);
        }
        [HttpGet, Authorize]
        [Route("daily/{groupId}/{year}/{month}/{day}")]
        public ActionResult<DayAttendanceDto> GetChildAttendance(int groupId, int year, int month, int day)
        {
            var child = _groupRepo.GetGroup(groupId);
            if (child == null)
            {
                return NotFound();
            }
            return Ok(_attendanceService.GetAttendance(child, year, month, day));
        }

        [HttpPost, Authorize]
        [Route("{groupId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public async Task<ActionResult<DayAttendanceDto>> SetAttendance(int groupId, int year, int month, int day, [FromBody] AttendanceRequestDto dto)
        {
            var child = _groupRepo.GetGroup(groupId);
            if (child == null)
            {
                return NotFound();
            }
            await _attendanceService.SetAttendance(child, dto, year, month, day);
            return Ok(null);
        }
    }
}