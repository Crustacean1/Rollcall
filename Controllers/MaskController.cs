using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Rollcall.Repositories;
using Rollcall.Services;
using Rollcall.Models;

namespace Rollcall.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaskController : ControllerBase
    {
        private readonly IChildRepository _childRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly MaskHandlerService _maskService;
        private readonly ILogger<MaskController> _logger;
        private readonly IAttendanceParserService _mealParser;
        public MaskController(MaskHandlerService maskService, IChildRepository childRepo, IGroupRepository groupRepo, ILogger<MaskController> logger)
        {
            _logger = logger;
            _maskService = maskService;
            _childRepo = childRepo;
            _groupRepo = groupRepo;
        }
        [HttpPost, Authorize]
        [Route("{groupId}/{year}/{month}/{day}")]
        public async Task<ActionResult<AttendanceDto[]>> SetMask(int groupId, int year, int month, int day, [FromBody] Dictionary<string, bool> meals)
        {
            var mask = await _maskService.SetMask(groupId, year, month, day, meals);
            return Ok(new AttendanceDto[]{
            new AttendanceDto{
                Meals = mask,
                Date = new MealDate { Year = year, Month = month, Day = day }
            }});
        }
        [HttpGet, Authorize]
        [Route("group/{groupId}/{year}/{month}/{day?}")]
        public ActionResult<List<AttendanceDto>> GetMask(int groupId, int year, int month, int day)
        {
            return Ok(_maskService.GetMasks(groupId, year, month, day));
        }

        [HttpGet, Authorize]
        [Route("child/{childId}/{year}/{month}/{day?}")]
        public ActionResult<List<AttendanceDto>> GetChildMask(int childId, int year, int month, int day)
        {
            var child = _childRepo.GetChild(childId);
            if (child == null)
            {
                return NotFound();
            }
            return GetMask(child.GroupId, year, month, day);
        }

        [HttpPost, Authorize]
        [Route("prolong/{groupId?}/{year}/{month}")]
        public async Task<ActionResult> ProlongGroup(int groupId, int year, int month)
        {
            if (groupId == 0)
            {
                var groups = _groupRepo.GetGroups();
                foreach (var group in groups)
                {
                    await _maskService.ProlongMasks(group.Id, year, month);
                }
                return Ok();
            }
            await _maskService.ProlongMasks(groupId, year, month);
            return Ok();
        }

    }
}