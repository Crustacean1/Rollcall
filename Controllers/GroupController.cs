using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Rollcall.Repositories;
using Rollcall.Models;

namespace Rollcall.Controllers
{
    [ApiController]
    [Route("group")]
    public class GroupController : ControllerBase
    {
        private readonly GroupRepository _repository;
        public GroupController(GroupRepository repository)
        {
            _repository = repository;
        }
        [HttpPost, Authorize]
        public async Task<ActionResult<GroupDto>> AddGroup([FromBody] GroupDto dto)
        {
            if (dto.Name == null)
            {
                return BadRequest();
            }
            var prevGroup = _repository.GetGroup(dto.Name);
            if (prevGroup != null)
            {
                return BadRequest();
            }
            var group = new Group { Name = dto.Name };
            _repository.AddGroup(group);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(null, new GroupDto { Name = group.Name, Id = group.Id });
        }
        [HttpGet, Authorize]
        [Route("{groupId}")]
        public ActionResult<GroupDto> GetGroup(int groupId)
        {
            if (groupId == 0)
            {
                return Ok(new GroupDto
                {
                    Name = "Wszystkie",
                    Id = 0
                });
            }
            var group = _repository.GetGroup(groupId);
            if (group == null)
            {
                return NotFound();
            }
            return new GroupDto
            {
                Name = group.Name,
                Id = group.Id
            };
        }
        [HttpGet, Authorize]
        public ActionResult<List<GroupResultDto>> GetGroups()
        {
            var groups = _repository.GetGroups();
            return groups.Select(group => new GroupResultDto { Name = group.Name, Id = group.Id }).ToList();
        }
    }
}