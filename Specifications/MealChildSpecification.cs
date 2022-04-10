using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class TotalChildSpecification : ISpecification<Child>
    {
        public bool Tracking { get; set; }
        public Expression<Func<Child, bool>> Condition { get; set; }
        public IEnumerable<Expression<Func<Child, object>>> Includes { get; set; }
        public TotalChildSpecification(int childId, bool isTracking = false)
        {
            Condition = (Child c) => c.Id == childId;
            Includes = new List<Expression<Func<Child, object>>> {
                (Child c) => c.DefaultMeals,
                (Child c) =>c.MyGroup };
            Tracking = isTracking;
        }
    }
}