
using System.Linq.Expressions;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class GroupMaskRepository : MaskRepositoryBase, IMaskRepository<Group>
    {
        public GroupMaskRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<MaskEntity> GetMonthlyMasks(Group target, int year, int month)
        {
            Expression<Func<GroupAttendance, bool>> targetCondition = c => c.GroupId == target.Id;
            Expression<Func<GroupAttendance, bool>> dateCondition = c => c.Date.Year == year && c.Date.Month == month;
            var result = ConstructQuery(targetCondition, dateCondition);
            return result;
        }
        public IEnumerable<MaskEntity> GetDailyMask(Group target, int year, int month, int day)
        {
            Expression<Func<GroupAttendance, bool>> targetCondition = c => c.GroupId == target.Id;
            Expression<Func<GroupAttendance, bool>> dateCondition = c => c.Date.Year == year && c.Date.Month == month && c.Date.Day == day;
            var result = ConstructQuery(targetCondition, dateCondition);
            return result;
        }
    }
}