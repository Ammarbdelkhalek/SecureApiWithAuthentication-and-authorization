using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace SecureApiWithAuthentication.filters
{
    public class FilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("sensitve action executed !!!!!!!");
        }

    }
}
