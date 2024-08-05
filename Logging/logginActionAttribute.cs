using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;

namespace SecureApiWithAuthentication.Logging
{
    public class logginActionAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Result is ViewResult result)
            {
                if(result.Model is  LoggingModel model ) {
                    model.TransFormData();
                
                }
            }
            base.OnActionExecuted(context);
        }

    }
}
