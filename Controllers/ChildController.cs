using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildController : ControllerBase
    {
        private readonly ChildService _childService;
        private readonly ILogger<ChildController> _logger;

        public ChildController(ILogger<ChildController> logger, ChildService childService)
        {
            _logger = logger;
            _childService = childService;
        }

        [HttpGet, Authorize]
        [Route("{childId}")]
        public ActionResult<ChildDto> GetChild(int childId)
        {
            var child = _childService.GetChildDto(childId);
            if (child is null)
            {
                return NotFound();
            }
            return Ok(child);
        }

        [HttpGet, Authorize]
        public ActionResult<ICollection<ChildDto>> GetChildren()
        {
            var children = _childService.GetChildrenFromGroup(0);
            if (children is null)
            {
                return NotFound();
            }
            return Ok(children);
        }

        [HttpGet, Authorize]
        [Route("group/{groupId}")]
        public ActionResult<ICollection<ChildDto>> GetChildren(int groupId)
        {
            var children = _childService.GetChildrenFromGroup(groupId);
            if (children is null)
            {
                return NotFound();
            }
            return Ok(children);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<int>> AddChildren([FromBody] ChildCreationDto childrenDto)
        {
            var child = await _childService.AddChild(childrenDto);
            if (child is null)
            {
                return BadRequest("Invalid child data specified");
            }
            return Ok(child.Id);
        }


        [HttpPatch, Authorize]
        [Route("{childId}")]
        public async Task<ActionResult<IEnumerable<AttendanceRequestDto>>> UpdateDefaultMeal(int childId, [FromBody] IDictionary<string, bool> update)
        {
            var newDefault = await _childService.UpdateChild(childId, update);
            if (newDefault is null)
            {
                _logger.LogInformation("Returning not found");
                return NotFound();
            }
            return Ok(newDefault);
        }

        [HttpPut, Authorize]
        [Route("{childId}")]
        public async Task<ActionResult<ChildResponseDto>> UpdateChild(int childId, ChildUpdateDto dto)
        {
            var child = await _childService.UpdateChild(childId, dto);
            if (child == null)
            {
                return NotFound();
            }
            return Ok(child);
        }

        [HttpDelete, Authorize]
        [Route("{childId}")]
        public async Task<ActionResult> RemoveChildren(int childId)
        {
            var isRemoved = await _childService.RemoveChild(childId);
            if (!isRemoved)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
