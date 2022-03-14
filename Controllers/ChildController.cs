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
        private readonly SchemaService _schemaService;
        private readonly ILogger<ChildController> _logger;

        private Child Parse(ChildDto dto)
        {
            var defaultAttendance = new List<DefaultAttendance>();
            foreach (var meal in dto.DefaultAttendance)
            {
                defaultAttendance.Add(new DefaultAttendance
                {
                    MealId = _schemaService.Translate(meal.Key),
                    Attendance = meal.Value
                });
            }
            return new Child
            {
                Name = dto.Name,
                Surname = dto.Surname,
                DefaultMeals = defaultAttendance,
                GroupId = dto.GroupId
            };
        }
        private ChildDto Marshall(Child child)
        {
            _logger.LogInformation($"My group: {child.MyGroup.Name}");
            var list = child.DefaultMeals.ToList();
            _logger.LogInformation($"My attendance {list[0].MealId}");
            return new ChildDto
            {
                GroupName = child.MyGroup.Name,
                Name = child.Name,
                Surname = child.Surname,
                DefaultAttendance = child.DefaultMeals.ToDictionary(
                   d => _schemaService.Translate(d.MealId),
                d => d.Attendance
                ),
                GroupId = child.GroupId,
                Id = child.Id
            };
        }

        public ChildController(ILogger<ChildController> logger, ChildRepository childRepository,
         SchemaService schemaService,
         GroupRepository groupRepository)
        {
            _logger = logger;
            _childRepository = childRepository;
            _schemaService = schemaService;
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
        public async Task<ActionResult<int[]>> AddChildren([FromBody] ICollection<ChildDto> childrenDto)
        {
            _logger.LogInformation("Adding children");
            var children = childrenDto.Select(e => Parse(e));
            _childRepository.AddChildren(children);
            await _childRepository.SaveChangesAsync();
            return Ok(children.Select(c => c.Id));
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