using System.Linq.Expressions;
using Rollcall.Models;

namespace Rollcall.Specifications
{
    public class BaseGroupSpecification : ISpecification<Group>
    {
        public bool Tracking { get; }
        public Expression<Func<Group, bool>> Condition { get; }
        public IEnumerable<Expression<Func<Group, object>>> Includes { get; }

        public BaseGroupSpecification(string name, bool tracking = false)
        {
            Tracking = tracking;
            Condition = (Group g) => g.Name == name;
            Includes = new List<Expression<Func<Group, object>>>();
        }
        public BaseGroupSpecification(int id, bool tracking = false)
        {
            Tracking = tracking;
            Condition = id == 0 ? (Group g) => true : (Group g) => g.Id == id;
            Includes = new List<Expression<Func<Group, object>>>();
        }
    }
}