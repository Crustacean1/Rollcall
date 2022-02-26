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
        private readonly ChildRepository _childRepo;
        private readonly MaskHandlerService _maskService;
        private readonly ILogger<MaskController> _logger;
        private readonly IMealParserService _mealParser;
        public MaskController(MaskHandlerService maskService, ChildRepository childRepo, ILogger<MaskController> logger)
        {
            _logger = logger;
            _maskService = maskService;
            _childRepo = childRepo;
        }
        [HttpPost, Authorize]
        [Route("{groupId}/{year}/{month}/{day}")]
        public async Task<ActionResult> SetMask(int groupId, int year, int month, int day, [FromBody] Dictionary<string, bool> meals)
        {
            await _maskService.SetMask(groupId, year, month, day, meals);
            return Ok();
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

    }
}