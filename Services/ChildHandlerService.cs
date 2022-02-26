using Rollcall.Models;

namespace Rollcall.Services
{
    public class ChildHandlerService
    {
        private readonly IMealParserService _parserService;
        public ChildHandlerService(IMealParserService parserService)
        {
            _parserService = parserService;
        }
        public ChildDto ToDto(Child child)
        {
            return new ChildDto
            {
                Name = child.Name,
                Surname = child.Surname,
                GroupId = child.GroupId,
                GroupName = (child.MyGroup == null) ? "" : child.MyGroup.Name,
                DefaultMeals = _parserService.ToDict(child.DefaultMeals)
            };
        }
        public Child FromDto(ChildDto dto)
        {
            return new Child
            {
                Name = dto.Name,
                Surname = dto.Surname,
                GroupId = dto.GroupId,
                DefaultMeals = _parserService.FromDict(dto.DefaultMeals)
            };
        }
    }
}