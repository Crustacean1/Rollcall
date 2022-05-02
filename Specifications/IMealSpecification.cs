using System.Linq.Expressions;

namespace Rollcall.Specifications
{
    public interface IMealSpecification<T>
    {
        bool Tracking { get; }
        Expression<Func<T, bool>> Condition { get; }
        IEnumerable<Expression<Func<T, object>>> Grouping { get; }
    }
}