using System.Linq.Expressions;

namespace Rollcall.Specifications
{
    public interface ISpecification<T>
    {
        public bool Tracking { get; set; }
        Expression<Func<T, bool>> Condition { get; }
        IEnumerable<Expression<Func<T, object>>> Includes { get; }
    }
}