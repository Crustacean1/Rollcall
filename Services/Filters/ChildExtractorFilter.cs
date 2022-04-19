using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Rollcall.Repositories;
using Rollcall.Specifications;

namespace Rollcall.Services
{
    public class ChildExtractorFilter : IActionFilter
    {
        private readonly ChildRepository _childRepo;
        public ChildExtractorFilter(ChildRepository childRepo)
        {
            _childRepo = childRepo;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpParams = context.ActionArguments;
            if (!httpParams.ContainsKey("childId"))
            {
                context.Result = new BadRequestResult();
            }
            var child = _childRepo.GetChild(new BaseChildSpecification((int)httpParams["childId"]));
            if (child is null)
            {
                context.Result = new NotFoundResult();
            }
            context.HttpContext.Items.Add("child", child);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}