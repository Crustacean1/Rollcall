using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Rollcall.Repositories;
using Rollcall.Models;
using Rollcall.Services;

namespace Rollcall.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildController : ControllerBase
    {
        private readonly IChildRepository _childRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ChildHandlerService _childService;
        private readonly ILogger<ChildController> _logger;

        public ChildController(ILogger<ChildController> logger, IChildRepository childRepository,
         IGroupRepository groupRepository, ChildHandlerService childService)
        {
            _logger = logger;
            _childRepository = childRepository;
            _groupRepository = groupRepository;
            _childService = childService;
        }

        [HttpGet, Authorize]
        [Route("{childId}")]
        public ActionResult<ChildDto> GetChild(int childId)
        {
            var child = _childRepository.GetChild(childId);
            if (child == null)
            {
                return NotFound();
            }
            return Ok(_childService.ToDto(child));
        }

        [HttpGet, Authorize]
        [Route("group/{groupId}")]
        public ActionResult<ICollection<ChildDto>> GetChildren(int groupId)
        {
            ICollection<Child> children;
            children = _childRepository.GetChildrenByGroup(groupId);

            return Ok(children.Select(child => _childService.ToDto(child)).ToList());
        }

        [HttpGet, Authorize]
        public ActionResult<ICollection<ChildDto>> GetChildren()
        {
            ICollection<Child> children;
            children = _childRepository.GetChildrenByGroup();

            return Ok(children.Select(child => _childService.ToDto(child)).ToList());
        }

        [HttpPost, Authorize]
        public async Task<ActionResult> AddChildren([FromBody] ICollection<ChildDto> childrenDto)
        {
            _logger.LogInformation("Adding children");
            var children = childrenDto.Select(e => _childService.FromDto(e));
            _childRepository.AddChildren(children);
            await _childRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete, Authorize]
        [Route("{Id}")]
        public async Task<ActionResult> RemoveChildren(int Id)
        {
            _logger.LogInformation($"Deleting children with ID {Id}");
            var child = _childRepository.GetChild(Id);
            if (child == null)
            {
                return NotFound();
            }
            _childRepository.RemoveChild(child);
            await _childRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}