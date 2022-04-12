using Rollcall.Models;
using System.Collections;

namespace Rollcall.Services
{
    public interface IChildService
    {

        public IEnumerable<ChildDto>? GetChildrenFromGroup(int groupId);
        public ChildDto? GetChildDto(int childId);
        public Task<ChildResponseDto?> AddChild(ChildCreationDto dto);
        public Task<ChildResponseDto?> UpdateChild(int childId, ChildUpdateDto dto);
        public Task<IDictionary<string, bool>?> UpdateChild(int childId, IDictionary<string, bool> newDto);
        public Task<bool> RemoveChild(int childId);
    }
}