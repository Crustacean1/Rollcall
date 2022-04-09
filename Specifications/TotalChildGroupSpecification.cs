using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class TotalChildGroupSpecification : ISpecification<Child>
    {
        public bool Tracking { get; set; }
        public Expression<Func<Child, bool>> Condition { get; set; }
        public IEnumerable<Expression<Func<Child, object>>> Includes { get; set; }
        public TotalChildGroupSpecification(int groupId = 0, bool isTracking = false)
        {
            Condition = groupId != 0 ? (Child c) => c.GroupId == groupId : (Child c) => true;
            Includes = new List<Expression<Func<Child, object>>> {
                (Child c) => c.DefaultMeals,
                (Child c) => c.MyGroup };
            Tracking = isTracking;
        }
    }
}