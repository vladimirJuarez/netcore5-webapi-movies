using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace back_end.Filters
{
    public class MyFilterAction: IActionFilter
    {
        private readonly ILogger<MyFilterAction> _logger;

        public MyFilterAction(ILogger<MyFilterAction> logger)
        {
            this._logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("before executes action");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("executed action");
        }
    }
}