
using System.Linq.Expressions;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class GroupMaskRepository : MaskRepositoryBase
    {
        public GroupMaskRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<MaskEntity> GetMasks(Group? target, int year, int month, int day = 0)
        {
            if (target == null)
            {
                return GeneralQuery(DateCondition(year, month, day));
            }
            var result = ConstructQuery(TargetCondition(target), DateCondition(year, month, day));
            return result;
        }
        public Expression<Func<GroupAttendance, bool>> TargetCondition(Group group)
        {
            return g => g.GroupId == group.Id;
        }
    }
}