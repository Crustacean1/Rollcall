using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Rollcall.Services
{
    public class DateValidationFilter : IActionFilter
    {
        private readonly ILogger<DateValidationFilter> _logger;
        public DateValidationFilter(ILogger<DateValidationFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var parameters = context.ActionArguments;
            if (!parameters.ContainsKey("year")|| !parameters.ContainsKey("month"))
            {
                _logger.LogInformation("Incomplete date provided");
                context.Result = new BadRequestResult();
                return;
            }
            int year = (int)parameters["year"];
            int month = (int)parameters["month"];
            int day = parameters.ContainsKey("day") ? (int)parameters["day"] : 1;
            _logger.LogInformation($"DateValidationService: {day}/{month}/{year}");
            try
            {
                var date = new DateTime(year, month, day);
                if (date > DateTime.Now)
                {
                    context.Result = new BadRequestResult();
                    return;
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                context.Result = new BadRequestResult();
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}