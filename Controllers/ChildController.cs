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
        private readonly ChildRepository _childRepository;
        private readonly IMealParserService _mealParser;
        private readonly ILogger<ChildController> _logger;

        private Child Parse(ChildDto dto)
        {
            return new Child
            {
                Name = dto.Name,
                Surname = dto.Surname,
                DefaultMeals = _mealParser.Parse(dto.DefaultAttendance),
                GroupId = dto.GroupId,
            };
        }
        private ChildDto Marshall(Child child)
        {
            return new ChildDto
            {
                GroupName = child.MyGroup.Name,
                Name = child.Name,
                Surname = child.Surname,
                DefaultAttendance = _mealParser.Marshall(child.DefaultMeals),
                GroupId = child.GroupId,
                Id = child.Id
            };
        }

        public ChildController(ILogger<ChildController> logger, ChildRepository childRepository,
         GroupRepository groupRepository, IMealParserService mealParser)
        {
            _logger = logger;
            _childRepository = childRepository;
            _mealParser = mealParser;
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
            return Ok(Marshall(child));
        }

        [HttpGet, Authorize]
        [Route("group/{groupId}")]
        public ActionResult<ICollection<ChildDto>> GetChildren(int groupId)
        {
            ICollection<Child> children;
            children = _childRepository.GetChildrenByGroup(groupId);

            return Ok(children.Select(child => Marshall(child)).ToList());
        }

        [HttpGet, Authorize]
        public ActionResult<ICollection<ChildDto>> GetChildren()
        {
            var children = _childRepository.GetChildrenByGroup().ToList();

            return Ok(children.Select(child => Marshall(child)).ToList());
        }

        [HttpPost, Authorize]
        public async Task<ActionResult> AddChildren([FromBody] ICollection<ChildDto> childrenDto)
        {
            _logger.LogInformation("Adding children");
            var children = childrenDto.Select(e => Parse(e));
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