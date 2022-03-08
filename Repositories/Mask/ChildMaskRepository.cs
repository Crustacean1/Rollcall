using System.Linq.Expressions;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class ChildMaskRepository : MaskRepositoryBase, IMaskRepository<Child>
    {
        public ChildMaskRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<MaskEntity> GetMonthlyMasks(Child target, int year, int month)
        {
            Expression<Func<GroupAttendance, bool>> targetCondition = c => c.GroupId == target.GroupId;
            Expression<Func<GroupAttendance, bool>> dateCondition = c => c.Date.Year == year && c.Date.Month == month;
            var result = ConstructQuery(targetCondition, dateCondition);
            return result;
        }
        public IEnumerable<MaskEntity> GetDailyMask(Child target, int year, int month, int day)
        {
            Expression<Func<GroupAttendance, bool>> targetCondition = c => c.GroupId == target.GroupId;
            Expression<Func<GroupAttendance, bool>> dateCondition = c => c.Date.Year == year && c.Date.Month == month && c.Date.Day == day;
            var result = ConstructQuery(targetCondition, dateCondition);
            return result;
        }
    }
}