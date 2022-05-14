using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Rollcall.Services
{
    public class DateValidationFilterBase
    {
        protected DateTime? parseContext(ActionExecutingContext context)
        {
            var parameters = context.ActionArguments;
            if (!parameters.ContainsKey("year") || !parameters.ContainsKey("month"))
            {
                context.Result = new BadRequestResult();
                return null;
            }
            int year = (int)parameters["year"];
            int month = (int)parameters["month"];
            int day = parameters.ContainsKey("day") ? (int)parameters["day"] : 1;
            try
            {
                var date = new DateTime(year, month, day);
                return date;
            }
            catch (ArgumentOutOfRangeException e)
            {
                context.Result = new BadRequestResult();
            }
            return null;
        }
    }
    public class DateValidationFilter : DateValidationFilterBase, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var date = parseContext(context);
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
    public class MealUpdateDateFilter : DateValidationFilterBase, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            DateTime? date = parseContext(context);
            var a = new DateTime();

            if (date is null || date > DateTime.Now || ((DateTime)date).DayOfWeek == DayOfWeek.Sunday || ((DateTime)date).DayOfWeek == DayOfWeek.Saturday)
            {
                context.Result = new BadRequestResult();
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}