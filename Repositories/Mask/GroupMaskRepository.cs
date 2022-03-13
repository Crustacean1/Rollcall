
using System.Linq.Expressions;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class GroupMaskRepository : MaskRepositoryBase
    {
        public GroupMaskRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<MaskEntity> GetMasks(Group target, int year, int month)
        {
            Expression<Func<GroupAttendance, bool>> dateCondition = c => c.Date.Year == year && c.Date.Month == month;
            var result = ConstructQuery(TargetCondition(target), dateCondition);
            return result;
        }
        public IEnumerable<MaskEntity> GetMask(Group target, int year, int month, int day)
        {
            Expression<Func<GroupAttendance, bool>> dateCondition = c => c.Date.Year == year && c.Date.Month == month && c.Date.Day == day;
            var result = ConstructQuery(TargetCondition(target), dateCondition);
            return result;
        }
        public Expression<Func<GroupAttendance, bool>> TargetCondition(Group? group)
        {
            if (group == null)
            {
                return g => true;
            }
            return g => g.GroupId == group.Id;
        }
    }
}