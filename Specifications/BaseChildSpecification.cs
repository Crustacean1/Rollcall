using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class BaseChildSpecification : ISpecification<Child>
    {
        public bool Tracking { get; set; }
        public Expression<Func<Child, bool>> Condition { get; set; }
        public IEnumerable<Expression<Func<Child, object>>> Includes { get; set; }
        public BaseChildSpecification(int childId, bool isTracking = false)
        {
            Condition = (Child c) => c.Id == childId;
            Includes = new List<Expression<Func<Child, object>>> { };
            Tracking = isTracking;
        }
    }
}