using System.Linq.Expressions;

using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Specifications
{
    public interface ISummarySpecification<GroupingType, ResultType>
    {
        public bool Masked { get; }
        public Expression<Func<ChildMeal, bool>> Condition { get; }
        public IEnumerable<string> Includes { get; }
        public Expression<Func<ChildMeal, GroupingType>> Grouping { get; }
        public Expression<Func<IGrouping<GroupingType, ChildMeal>, ResultType>> Selection { get; }
    }
}