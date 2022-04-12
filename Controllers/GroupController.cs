using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Rollcall.Services;
using Rollcall.Models;

namespace Rollcall.Controllers
{
    [ApiController]
    [Route("group")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupController> _logger;
        public GroupController(IGroupService groupService, ILogger<GroupController> logger)
        {
            _groupService = groupService;
            _logger = logger;
        }
        [HttpPost, Authorize]
        public async Task<ActionResult<GroupDto>> AddGroup([FromBody] GroupCreationDto dto)
        {
            var group = await _groupService.CreateGroup(dto);
            if (group == null)
            {
                return BadRequest("Invalid params for group creation, check if such group does not already exist");
            }
            return CreatedAtAction(null, group);
        }
        [HttpGet, Authorize]
        [Route("{groupId}")]
        public ActionResult<GroupDto> GetGroup(int groupId)
        {
            var group = _groupService.GetGroup(groupId);
            if (group == null)
            {
                return NotFound();
            }
            return group;
        }
        [HttpPut, Authorize]
        [Route("{groupId}")]
        public async Task<ActionResult<GroupDto>> UpdateGroupName(int groupId, [FromBody] GroupUpdateDto dto)
        {
            var group = await _groupService.RenameGroup(groupId, dto);
            if (group == null)
            {
                return NotFound();
            }
            return group;
        }

        [HttpGet, Authorize]
        public ActionResult<List<GroupResultDto>> GetAllGroups()
        {
            var groups = _groupService.GetAllGroups();
            return Ok(groups.ToList());
        }
    }
}