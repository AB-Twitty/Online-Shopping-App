using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shopping.VM;
using Shopping.VM.AccountVM;

namespace Shopping.API.Helpers
{
    public class IsDeletedAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = (AccountInfoVM)context.HttpContext.Items["User"];
            if (user == null || user.isDeleted)
            {
                context.Result = new ObjectResult(new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Data = false,
                    Message = "Your Account is Deleted, Contact us to restore it.",
                    Token = context.HttpContext.Request.Headers["Authorization"].First().Split(" ").Last()
                });
            }
        }
    }
}
