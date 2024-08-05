using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SecureApiWithAuthentication.Autentication;
using SecureApiWithAuthentication.Data;
using System.Security.Claims;

namespace SecureApiWithAuthentication.Authorization
{
    public class permissionBasedAuthorization(AppDbcontext dbcontext) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var attribute =(authenticationbasedPermissionsAttribute) context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is authenticationbasedPermissionsAttribute);
            if (attribute != null)
            {
                var ClaimIdenetity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (ClaimIdenetity == null || !ClaimIdenetity.IsAuthenticated) {
                    context.Result = new ForbidResult();
                }
                else
                {
                    var userId = int.Parse(ClaimIdenetity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var HasPermission = dbcontext.Permissions.Any(x => x.userId == userId 
                    && x.PermissionId == attribute.Permission);
                    if(!HasPermission) 
                    { 
                        context.Result = new ForbidResult();
                    }
                }

            }
        }
    }
}
