using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Rollcall.Repositories;
using Rollcall.Specifications;
using Rollcall.Models;

namespace Rollcall.Services
{
    public class GroupExtractorFilter : IActionFilter
    {
        private readonly GroupRepository _groupRepo;
        public GroupExtractorFilter(GroupRepository groupRepo)
        {
            _groupRepo = groupRepo;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpParams = context.ActionArguments;
            if (!httpParams.ContainsKey("groupId"))
            {
                context.Result = new BadRequestResult();
            }
            var groupId = (int)httpParams["groupId"];
            var group = groupId == 0 ? null : _groupRepo.GetGroup(new BaseGroupSpecification(groupId));
            if (group is null && groupId != 0)
            {
                context.Result = new NotFoundResult();
            }
            context.HttpContext.Items.Add("group", group);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}