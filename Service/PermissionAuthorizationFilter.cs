using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ProductWebApi.Service
{
    public class PermissionAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IPermissionService _permissionService;

        public PermissionAuthorizationFilter(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //skip anonymous
            if (context.ActionDescriptor.EndpointMetadata.Any(e => e is AllowAnonymousAttribute))
                return;

            var user = context.HttpContext.User;
            if(!user.Identity.IsAuthenticated)
            {
                context.Result= new UnauthorizedResult();
                return;
            } 

            var role = user.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "Admin") return;

            //Get controller name
            var controller = context.RouteData.Values["controller"]?.ToString(); 
            var method = context.HttpContext.Request.Method; // Get method name

            //string action = method switch
            //{
            //    "GET" => "View",
            //    "POST" => "Create",
            //    "PUT" => "Update",
            //    "DELETE" => "Delete",
            //    _ => null
            //};

            //if (action == null)
            //{
            //    context.Result = new ForbidResult();
            //    return;
            //}

            var permission = $"{controller}.{method}";

            var allowed =await _permissionService.HasPermission(role, permission);
            if (!allowed)
            {
                context.Result = new ForbidResult();
                
            }


        }
    }
}
