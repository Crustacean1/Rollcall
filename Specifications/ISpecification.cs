using System.Linq.Expressions;

namespace Rollcall.Specifications
{
    public interface ISpecification<T>
    {
        public bool Tracking { get; }
        Expression<Func<T, bool>> Condition { get; }
        IEnumerable<Expression<Func<T, object>>> Includes { get; }
        IEnumerable<Expression<Func<T, object>>> Groups { get; }
    }
}