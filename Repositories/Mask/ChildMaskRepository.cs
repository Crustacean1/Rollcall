using System.Linq.Expressions;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class ChildMaskRepository : MaskRepositoryBase
    {
        public ChildMaskRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<MaskEntity> GetMasks(Child target, int year, int month)
        {
            Expression<Func<GroupAttendance, bool>> dateCondition = c => c.Date.Year == year && c.Date.Month == month;
            var result = ConstructQuery(TargetCondition(target), dateCondition);
            return result;
        }
        public IEnumerable<MaskEntity> GetMask(Child target, int year, int month, int day)
        {
            Expression<Func<GroupAttendance, bool>> dateCondition = c => c.Date.Year == year && c.Date.Month == month && c.Date.Day == day;
            var result = ConstructQuery(TargetCondition(target), dateCondition);
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