using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Rollcall.Models;
using Rollcall.Repositories;
using Rollcall.Services;

namespace Rollcall.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly ILogger<AttendanceController> _logger;
        private readonly AttendanceHandlerService _attendanceHandler;
        public AttendanceController(ILogger<AttendanceController> logger, AttendanceHandlerService attendanceHandler)
        {
            _logger = logger;
            _attendanceHandler = attendanceHandler;
        }

        [HttpGet, Authorize]
        [Route("child/{childId}/{year}/{month}/{day?}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<List<ChildAttendanceDto>> GetChildAttendance(int childId, int year, int month, int day)
        {
            try
            {
                var childAttendanceCount = _attendanceHandler.GetChildAttendance(childId, year, month, day);
                return childAttendanceCount;
            }
            catch (InvalidDataException e)
            {
                return NotFound();
            }
        }

        [HttpGet, Authorize]
        [Route("group/{groupId}/{year}/{month}/{day?}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public ActionResult<List<GroupAttendanceDto>> GetGroupAttendance(int groupId, int year, int month, int day)
        {
            try
            {
                _logger.LogInformation("Getting group attendance");
                return Ok(_attendanceHandler.GetGroupAttendance(groupId, year, month, day));
            }
            catch (InvalidDataException e)
            {
                return NotFound();
            }
        }

        [HttpPost, Authorize]
        [Route("child/{childId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public async Task<ActionResult> SetAttendance(int childId, int year, int month, int day, [FromBody] Dictionary<string, bool> dto)
        {
            try
            {
                try
                {
                    await _attendanceHandler.SetChildAttendance(childId, year, month, day, dto);
                }
                catch (InvalidDataException e)
                {
                    return NotFound("No such user");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest("Date is not specified");
            }
            return Ok();
        }

        [HttpPost, Authorize]
        [Route("group/{groupId}/{year}/{month}/{day}")]
        [ServiceFilter(typeof(DateValidationFilter))]
        public async Task<ActionResult> SetAttendanceMask(int groupId, int year, int month, int day, [FromBody] Dictionary<string, bool> mealDto)
        {
            _logger.LogInformation("Setting attendance mask");
            try
            {
                try
                {
                    await _attendanceHandler.SetGroupAttendanceMask(groupId, year, month, day, mealDto);
                }
                catch (InvalidDataException e)
                {
                    return NotFound();
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}