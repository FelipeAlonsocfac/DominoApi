using Domino.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

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
                context.Result = new NotFoundObjectResult(new ProblemDetails
                { 
                    Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4",
                    Title = "Incorrect Payload.",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = "Incorrect UserName or Password."
                });
            }
            if (method.Contains(nameof(UserController.SignUp)))
            {
                context.Result = new ConflictObjectResult(new ProblemDetails
                {
                    Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.8",
                    Title = "Incorrect Payload.",
                    Status = (int)HttpStatusCode.Conflict,
                    Detail = "User already exists."
                });
            }
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}