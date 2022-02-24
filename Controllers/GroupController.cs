using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Rollcall.Repositories;
using Rollcall.Models;

namespace Rollcall.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase{
        private readonly IGroupRepository _repository;
        public GroupController(IGroupRepository repository){
            _repository = repository;
        }
        [HttpPost,Authorize]
        public async Task<ActionResult> AddGroup([FromBody]GroupDto dto){
            if(dto.Name == null){
                return BadRequest();
            }
            var prevGroup = _repository.GetGroup(dto.Name);
            if(prevGroup != null){
                return BadRequest();
            }
            var group = new Group{Name = dto.Name};
            _repository.AddGroup(group);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(null,null);
        }
        [HttpGet,Authorize]
        public ActionResult<List<GroupResultDto>> GetGroups(){
            var groups = _repository.GetGroups();
            return groups.Select(group => new GroupResultDto{Name = group.Name, Id = group.Id}).ToList();
        }
    }
}