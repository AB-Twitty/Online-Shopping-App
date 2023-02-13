using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shopping.VM.AccountVM;
using Shopping.VM;
using System.Net;

namespace Shopping.API.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;
            var user = (AccountInfoVM)context.HttpContext.Items["User"];
            if (user == null || (_roles.Any() && !_roles.Contains(user.accountType)))
            {
                context.Result = new ObjectResult(new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Message = "Unauthorized",
                    Data = false,
                    Token = context.HttpContext.Request.Headers["Authorization"].First().Split(" ").Last()
                });
            }
        }
    }
}
