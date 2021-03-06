using Rollcall.Specifications;
using Rollcall.Repositories;
using Rollcall.Models;

namespace Rollcall.Services
{
    public class GroupService : IGroupService
    {
        ILogger<GroupService> _logger;
        GroupRepository _groupRepo;
        public GroupService(ILogger<GroupService> logger, GroupRepository groupRepo)
        {
            _logger = logger;
            _groupRepo = groupRepo;
        }
        public IEnumerable<GroupDto> GetAllGroups()
        {
            return _groupRepo.GetGroups().Select(g => new GroupDto
            {
                Name = g.Name,
                Id = g.Id
            });
        }
        public GroupDto? GetGroup(int groupId)
        {
            if (groupId == 0) { return new GroupDto { Name = "Wszystkie", Id = 0 }; }
            var group = _groupRepo.GetGroup(new BaseGroupSpecification(groupId));
            if (group == null) { return null; }
            return new GroupDto { Name = group.Name, Id = group.Id };
        }
        public async Task<GroupDto?> CreateGroup(GroupCreationDto dto)
        {
            if (_groupRepo.GetGroup(new BaseGroupSpecification(dto.Name)) != null)
            {
                return null;
            }
            var group = new Group { Name = dto.Name };
            _groupRepo.AddGroup(group);
            await _groupRepo.SaveChangesAsync();
            return new GroupDto { Name = group.Name, Id = group.Id };
        }
        public async Task<GroupDto?> RenameGroup(int groupId, GroupUpdateDto dto)
        {
            var group = _groupRepo.GetGroup(new BaseGroupSpecification(groupId, true));
            if (group == null || _groupRepo.GetGroup(new BaseGroupSpecification(dto.Name)) != null)
            {
                return null;
            }
            group.Name = dto.Name;
            await _groupRepo.SaveChangesAsync();
            return new GroupDto { Name = group.Name, Id = group.Id };
        }
    }
}