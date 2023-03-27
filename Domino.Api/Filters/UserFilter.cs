using Domino.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Domino.Api.Filters;

public class UserFilter : Attribute, IActionFilter
{  
    public void OnActionExecuted(ActionExecutedContext context)
    {
        string method = context.ActionDescriptor.DisplayName!;


        if (context.Result is OkObjectResult result && result.Value is null)
        {
            if (method.Contains(nameof(UserController.LogIn)))
            {
                context.Result = new NotFoundObjectResult("Incorrect UserName or Password");
            }
            if (method.Contains(nameof(UserController.SignUp)))
            {
                context.Result = new ConflictObjectResult("User already exists");
            }
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}