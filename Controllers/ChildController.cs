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
            var list = child.DefaultMeals.ToList();
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
        [HttpPost, Authorize]
        [Route("attendance/{childId}")]
        public async Task<ActionResult<IEnumerable<AttendanceRequestDto>>> UpdateDefaultMeal(int childId, [FromBody] IEnumerable<AttendanceRequestDto> update)
        {
            var child = _childRepository.GetChild(childId,true);
            if (child == null)
            {
                return NotFound();
            }
            var results = new List<AttendanceRequestDto>();
            foreach (var mealUpdate in update)
            {
                var currentId = _schemaService.Translate(mealUpdate.Name);
                _logger.LogInformation($"Adding entry: {mealUpdate.Name}");
                await _childRepository.AddDefaultMeal(child, new DefaultAttendance { MealId = currentId, Attendance = mealUpdate.Present });
            }
            return child.DefaultMeals
            .Select(m => new AttendanceRequestDto { Name = _schemaService.Translate(m.MealId), Present = m.Attendance }).ToList();
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