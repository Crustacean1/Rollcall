using System.Linq.Expressions;

using Rollcall.Models;

namespace Rollcall.Specifications
{
    public interface ISummarySpecification<GroupingType, ResultType> : IMealSpecification<GroupingType, ResultType>
    {
        public Expression<Func<Child, bool>> SummaryCondition { get; }
    }
}