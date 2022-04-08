using System.Linq.Expressions;

namespace Rollcall.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Condition { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}