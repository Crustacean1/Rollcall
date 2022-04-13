using System.Linq.Expressions;

using Rollcall.Models;
using Rollcall.Repositories;

namespace Rollcall.Specifications
{
    public interface ISummarySpecification<T>
    {
        public Expression<Func<ChildAttendance, bool>> MealCondition { get; }
        //public Expression<Func<GroupAttendance, bool>> MaskingCondition { get; }
        public Expression<Func<MealSummaryEntry, T>> Grouping { get; }
        public Expression<Func<IGrouping<T, MealSummaryEntry>, MealSummary>> Selection { get; }
        public bool Masked { get; }
    }
}