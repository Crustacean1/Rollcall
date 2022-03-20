using System.Linq.Expressions;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class ChildMaskRepository : MaskRepositoryBase
    {
        public ChildMaskRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<MaskEntity> GetMasks(Child target, int year, int month, int day = 0)
        {
            var result = ConstructQuery(TargetCondition(target), DateCondition(year, month, day));
            return result;
        }
        private Expression<Func<GroupAttendance, bool>> TargetCondition(Child? child)
        {
            if (child == null)
            {
                return c => true;
            }
            return c => c.GroupId == child.GroupId;
        }
    }
}