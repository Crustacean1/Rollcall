using Rollcall.Models;

namespace Rollcall.Services{
    public class ChildHandlerService{
        private readonly AttendanceHandlerService _parserService;
        public ChildHandlerService(AttendanceHandlerService parserService){
            _parserService = parserService; 
        }
        public ChildDto ToDto(Child child,ICollection<MealSchema> schemas){
            return new ChildDto{
                Name = child.Name,
                Surname = child.Surname,
                GroupId = child.GroupId,
                GroupName = (child.MyGroup == null) ? "" : child.MyGroup.Name,
                DefaultMeals = _parserService.ToDto(child.DefaultMeals,schemas)
            };
        }
        public Child FromDto(ChildDto dto,ICollection<MealSchema> schemas){
            return new Child{
                Name = dto.Name,
                Surname = dto.Surname,
                GroupId = dto.GroupId,
                DefaultMeals = _parserService.FromDto(dto.DefaultMeals,schemas)
            };
        }
    }
}