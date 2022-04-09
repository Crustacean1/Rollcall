using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class ChildService
    {
        ILogger<ChildService> _logger;

        ChildRepository _childRepo;
        GroupRepository _groupRepo;
        MealSchemaRepository _mealRepo;

        public ChildService(ChildRepository childRepo,
        GroupRepository groupRepo,
        MealSchemaRepository mealRepo,
        ILogger<ChildService> logger)
        {
            _childRepo = childRepo;
            _groupRepo = groupRepo;
            _mealRepo = mealRepo;
            _logger = logger;
        }

        public IEnumerable<ChildDto>? GetChildrenFromGroup(int groupId)
        {
            if (_groupRepo.GetGroup(groupId) is null)
            {
                return null;
            }
            var children = _childRepo.GetChildrenByGroup(groupId);
            return children.Select(c => new ChildDto
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                GroupId = c.GroupId,
                GroupName = c.MyGroup.Name,
                DefaultAttendance = c.DefaultMeals.ToDictionary(m => m.Schema.Name, m => m.Attendance)
            });
        }
        public ChildDto? GetChildDto(int childId)
        {
            var child = _childRepo.GetChild(childId);
            if (child is null)
            {
                return null;
            }
            _logger.LogInformation($"Found child: {child.Name} Surname: {child.Surname}");
            _logger.LogInformation($"DefaultAttendance size: {child.DefaultMeals.Count()}");
            return new ChildDto
            {
                Id = child.Id,
                Name = child.Name,
                Surname = child.Surname,
                GroupId = child.GroupId,
                DefaultAttendance = (child.DefaultMeals is null) ?
                new Dictionary<string, bool>() :
                 child.DefaultMeals.ToDictionary(m => (m.Schema is null) ? $"null{m.MealId.ToString()}" : m.Schema.Name, m => m.Attendance)
            };
        }
        public async Task<ChildResponseDto?> AddChild(ChildCreationDto dto)
        {
            var defaultMeals = ParseDefaultAttendance(dto.DefaultMeals);

            var child = new Child
            {
                Name = dto.Name,
                Surname = dto.Surname,
                DefaultMeals = defaultMeals
            };
            if (!IsValidChild(child))
            {
                return null;
            }
            _childRepo.AddChild(child);

            await _childRepo.SaveChangesAsync();
            return ToChildResponseDto(child);
        }
        public async Task<ChildResponseDto?> UpdateChild(int childId, ChildUpdateDto dto)
        {
            var child = _childRepo.GetChild(childId);
            if (child == null)
            {
                _logger.LogError("In ChildService: Cannot update nonexistent child");
                return null;
            }

            child.Name = dto.Name;
            child.Surname = dto.Surname;

            if (!IsValidChild(child))
            {
                return null;
            }

            await _childRepo.SaveChangesAsync();
            return ToChildResponseDto(child);
        }
        public async Task<IDictionary<string, bool>?> UpdateChild(int childId, IDictionary<string, bool> newDto)
        {
            var child = _childRepo.GetChild(childId, true);
            if (child is null)
            {
                _logger.LogError("In ChildService: Cannot update nonexistent child");
                return null;
            }

            var newAttendance = ParseDefaultAttendance(newDto);
            foreach(var meal in newAttendance){
            }

            foreach (var item in newDto)
            {
                _logger.LogInformation($"In update: {item.Key} : {item.Value}");
            }

            if (!HasValidMeal(child))
            {
                return null;
            }

            await _childRepo.SaveChangesAsync();
            return child.DefaultMeals.ToDictionary(m => (m.Schema is null) ? $"null{m.MealId}" : m.Schema.Name, m => m.Attendance);
        }
        public async Task<bool> RemoveChild(int id)
        {
            var child = _childRepo.GetChild(id);
            if (child is null)
            {
                return false;
            }
            _childRepo.RemoveChild(child);
            await _childRepo.SaveChangesAsync();
            return true;
        }
        private bool IsValidChild(Child child)
        {
            if (_groupRepo.GetGroup(child.GroupId) is null)
            {
                _logger.LogError("In ChildService: Cannot assign child to nonexistent group");
                return false;
            }

            if (child.Name is null || child.Surname is null)
            {
                _logger.LogError("In ChildService: Cannot change name or surname to null");
                return false;
            }

            return true;
        }
        private bool HasValidMeal(Child child)
        {
            _logger.LogInformation($"New attendance count: {child.DefaultMeals.Count()}");
            var difference = _mealRepo.CheckIfMealsExist(child.DefaultMeals.Select(m => m.MealId));
            _logger.LogInformation($"Difference: {difference}");
            if (child.DefaultMeals is not null && difference != 0)
            {
                _logger.LogError("In ChildService: Cannot set default attendance for nonexistent meal");
                return false;
            }
            return true;
        }
        private IEnumerable<DefaultAttendance> ParseDefaultAttendance(IDictionary<string, bool>? dto)
        {
            if (dto is null)
            {
                return new List<DefaultAttendance>();
            }
            var schemas = _mealRepo.ParseMeals(dto.Select(m => m.Key)).ToList();
            return schemas.Select(s => new DefaultAttendance
            {
                MealId = s.Id,
                Attendance = dto[s.Name]
            }).ToList();
        }
        private ChildResponseDto ToChildResponseDto(Child child)
        {
            return new ChildResponseDto
            {
                Name = child.Name,
                Surname = child.Surname,
                Id = child.Id,
                GroupId = child.GroupId,
            };
        }
    }
}