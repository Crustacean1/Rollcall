using Rollcall.Models;
namespace Rollcall.Services
{
    public interface IGroupService
    {
        public IEnumerable<GroupDto> GetAllGroups();
        public GroupDto? GetGroup(int groupId);
        public Task<GroupDto?> CreateGroup(GroupCreationDto dto);
        public Task<GroupDto?> RenameGroup(int groupId, GroupUpdateDto dto);
    }
}