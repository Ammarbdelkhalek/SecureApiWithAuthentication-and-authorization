using Microsoft.AspNetCore.Mvc.Filters;

namespace SecureApiWithAuthentication.filters
{
    public class FiltersAction : IActionFilter
    {
        private readonly ILogger<FiltersAction> logging;

        public FiltersAction(ILogger<FiltersAction> logging)
        {
            this.logging = logging;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //here execute action before the request 
            logging.LogInformation($"your action before is{context.ActionDescriptor.DisplayName} and the controller is {context.Controller} ");
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //here execute action after the request ** notes ** you can edit on the request 
            logging.LogInformation($"your action after {context.ActionDescriptor.DisplayName} and the controller is {context.Controller}");
            
        }

        
    }
}
