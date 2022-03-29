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
        private readonly ChildAttendanceService _attendanceService;
        public ChildAttendanceController(ILogger<ChildAttendanceController> logger,
        ChildAttendanceService attendanceService,
        ChildRepository childRepo)
        {
            _logger = logger;
            _attendanceService = attendanceService;
            _childRepo = childRepo;
        }

        [HttpGet, Authorize]
        [Route("count/{childId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<AttendanceCountDto> GetMonthlyCount(int childId, int year, int month)
        {
            var child = _childRepo.GetChild(childId);
            if (child == null)
            {
                return NotFound();
            }
            var result = _attendanceService.GetMonthlyCount(child, year, month);
            return Ok(result);
        }

        [HttpGet, Authorize]
        [Route("daily/{childId}/{year}/{month}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<List<DayAttendanceDto>> GetChildMonthlyAttendance(int childId, int year, int month)
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
        public ActionResult<DayAttendanceDto> GetChildAttendance(int childId, int year, int month, int day)
        {
            var child = _childRepo.GetChild(childId);
            if (child == null)
            {
                return NotFound();
            }
            return Ok(_attendanceService.GetDailyAttendance(child, year, month));
        }

        [HttpPost, Authorize]
        [Route("{childId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(FutureDateValidationFilter))]
        public async Task<ActionResult<List<DayAttendanceDto>>> SetAttendance(int childId, int year, int month, int day, [FromBody] List<AttendanceRequestDto> dto)
        {
            var child = _childRepo.GetChild(childId);
            if (child == null)
            {
                return NotFound();
            }
            var result = await _attendanceService.SetAttendance(child, dto, year, month, day);
            return Ok(result);
        }

        [HttpPost, Authorize]
        [Route("extend/{year}/{month}")]
        [ServiceFilter(typeof(FutureDateValidationFilter))]
        public async Task<ActionResult<ExtendResultDto>> ExtendChildrenAttendance(int year, int month)
        {
            var children = _childRepo.GetChildrenByGroup(0);
            var updated = await _attendanceService.ExtendAttendance(children, year, month);
            return new ExtendResultDto { Updated = updated };
        }
    }
}