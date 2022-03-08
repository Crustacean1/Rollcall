using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Rollcall.Models;
using Rollcall.Repositories;
using Rollcall.Services;

namespace Rollcall.Controllers
{
    [ApiController]
    [Route("attendance/child")]
    public class ChildAttendanceController : ControllerBase
    {
        private readonly ILogger<ChildAttendanceController> _logger;
        private readonly ChildRepository _childRepo;
        private readonly AttendanceService<Child> _attendanceService;
        public ChildAttendanceController(ILogger<ChildAttendanceController> logger,
        AttendanceService<Child> attendanceService,
        ChildRepository childRepo)
        {
            _logger = logger;
            _attendanceService = attendanceService;
            _childRepo = childRepo;
        }

        [HttpGet, Authorize]
        [Route("summary/{childId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<AttendanceDto> GetMonthlySummary(int childId, int year, int month)
        {
            var child = _childRepo.GetChild(childId);
            if (child == null)
            {
                return NotFound();
            }
            var result = _attendanceService.GetMonthlySummary(child, year, month);
            return Ok(result);
        }

        [HttpGet, Authorize]
        [Route("daily/{childId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<List<AttendanceDto>> GetChildMonthlyAttendance(int childId, int year, int month)
        {
            var child = _childRepo.GetChild(childId);
            if (child == null)
            {
                return NotFound();
            }
            var result = _attendanceService.GetMonthlyAttendance(child, year, month);
            return Ok(result);
        }
        [HttpGet, Authorize]
        [Route("daily/{childId}/{year}/{month}/{day}")]
        public ActionResult<AttendanceDto> GetChildAttendance(int childId, int year, int month, int day)
        {
            var child = _childRepo.GetChild(childId);
            if (child == null)
            {
                return NotFound();
            }
            return Ok(_attendanceService.GetAttendance(child, year, month, day));
        }

        [HttpPost, Authorize]
        [Route("{childId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public async Task<ActionResult<AttendanceDto>> SetAttendance(int childId, int year, int month, int day, [FromBody] AttendanceRequestDto dto)
        {
            var child = _childRepo.GetChild(childId);
            if (child == null)
            {
                return NotFound();
            }
            await _attendanceService.SetAttendance(child, dto, year, month, day);
            return Ok(null);
        }
    }
}