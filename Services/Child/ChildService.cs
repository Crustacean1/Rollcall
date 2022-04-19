using Rollcall.Helpers;
using Rollcall.Specifications;
using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Services
{
    public class ChildService : IChildService
    {
        DefaultAttendanceComparer _comparer;
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
            _comparer = new DefaultAttendanceComparer();
        }

        public IEnumerable<ChildDto>? GetChildrenFromGroup(int groupId)
        {
            if (groupId != 0 && _groupRepo.GetGroup(new BaseGroupSpecification(groupId)) is null)
            {
                return null;
            }
            var children = _childRepo.GetChildrenByGroup(new TotalChildGroupSpecification(groupId));
            return children.Select(c => new ChildDto
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                GroupId = c.GroupId,
                GroupName = c.MyGroup.Name,
                DefaultAttendance = c.DefaultMeals.ToDictionary(m => m.MealName, m => m.Attendance)
            });
        }
        public ChildDto? GetChildDto(int childId)
        {
            var child = _childRepo.GetChild(new TotalChildSpecification(childId));
            if (child is null)
            {
                return null;
            }
            _logger.LogInformation($"Found child: {child.Name} Surname: {child.Surname}");
            return new ChildDto
            {
                Id = child.Id,
                Name = child.Name,
                Surname = child.Surname,
                GroupId = child.GroupId,
                GroupName = child.MyGroup.Name,
                DefaultAttendance = (child.DefaultMeals is null) ?
                new Dictionary<string, bool>() :
                 child.DefaultMeals.ToDictionary(m => m.MealName, m => m.Attendance)
            };
        }
        public async Task<ChildResponseDto?> AddChild(ChildCreationDto dto)
        {
            if (!IsValidAttendance(dto.DefaultMeals))
            {
                return null;
            }

            var defaultMeals = dto.DefaultMeals is null ? new List<DefaultMeal>() :
             dto.DefaultMeals.Select(m => new DefaultMeal { MealName = m.Key, Attendance = m.Value });

            var child = new Child
            {
                Name = dto.Name,
                Surname = dto.Surname,
                GroupId = dto.GroupId,
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
            var child = _childRepo.GetChild(new TotalChildSpecification(childId, true));
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
            var child = _childRepo.GetChild(new TotalChildSpecification(childId, true));
            if (child is null || !IsValidAttendance(newDto))
            {
                _logger.LogError("In ChildService: invalid request data");
                return null;
            }

            var attendanceUpdate = newDto.Select(a => new DefaultMeal { MealName = a.Key, Attendance = a.Value });
            var newAttendance = attendanceUpdate.Union(child.DefaultMeals, _comparer).ToList();
            child.DefaultMeals = newAttendance;

            await _childRepo.SaveChangesAsync();
            return child.DefaultMeals.ToDictionary(m => m.MealName, m => m.Attendance);
        }
        public async Task<bool> RemoveChild(int childId)
        {
            var child = _childRepo.GetChild(new BaseChildSpecification(childId));
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
            if (_groupRepo.GetGroup(new BaseGroupSpecification(child.GroupId)) is null)
            {
                _logger.LogError($"In ChildService: Cannot assign child to nonexistent group: {child.GroupId}");
                return false;
            }

            if (child.Name is null || child.Surname is null)
            {
                _logger.LogError("In ChildService: Cannot change name or surname to null");
                return false;
            }

            return true;
        }
        private bool IsValidAttendance(IDictionary<string, bool>? attendance)
        {
            if (attendance is not null && _mealRepo.CountValidMeals(attendance.Select(m => m.Key)) != attendance.Count())
            {
                _logger.LogError("In ChildService: Cannot set default attendance for nonexistent meal");
                return false;
            }
            return true;
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
    class DefaultAttendanceComparer : IEqualityComparer<DefaultMeal>
    {
        public bool Equals(DefaultMeal? a, DefaultMeal? b)
        {
            return !(a is null || b is null || a.MealName != b.MealName);
        }
        public int GetHashCode(DefaultMeal a)
        {
            return a.MealName.GetHashCode();
        }
    }
}