using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.RegularExpressions;

namespace Domino.Api.Filters;

public class DominoFilter : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        List<string>? dominoes = GetDominoesFromRequest(context.ActionArguments!);
        if (!Validations(context, dominoes)) return;
    }    

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is OkObjectResult result && result.Value is IList<string> dominoes && dominoes.Count == 0)
        {
            //context.Result = new NoContentResult(); // this one may also be valid
            context.Result = BuildBadRequest($"Invalid parameter {nameof(dominoes)}.", $"Parameter {nameof(dominoes)} could not be ordered");
        }
    }

    private static List<string>? GetDominoesFromRequest(IDictionary<string, object> actionArguments)
    {
        if (!actionArguments.TryGetValue("dominoes", out var dominoesValue) || dominoesValue is not List<string> dominoes)
        {
            return null;
        }

        return dominoes;
    }

    private static bool Validations(ActionExecutingContext context, List<string>? dominoes)
    {
        if (dominoes is null)
        {
            context.Result = BuildBadRequest($"Invalid parameter {nameof(dominoes)}.", $"Parameter {nameof(dominoes)} cannot be null.");

            return false;
        }
        if (dominoes.Count < 2 || dominoes.Count > 6)
        {
            context.Result = BuildBadRequest($"Invalid parameter {nameof(dominoes)}.", $"Parameter {nameof(dominoes)} outside of bounds.");

            return false;
        }
        if (!dominoes.All(domino => Regex.IsMatch(domino, "^[0-6]\\|[0-6]$")))
        {
            context.Result = BuildBadRequest($"Invalid parameter {nameof(dominoes)}.", $"Each domino must follow the pattern '[0-6]|[0-6]'");

            return false;
        }
        if (dominoes.Count == 2 && !dominoes[0].Order().SequenceEqual(dominoes[1].Order()))
        {
            //context.Result = new NoContentResult(); // this one may also be valid
            context.Result = BuildBadRequest($"Invalid parameter {nameof(dominoes)}.", $"Parameter {nameof(dominoes)} could not be ordered");

            return false;
        }

        return true;
    }

    private static BadRequestObjectResult BuildBadRequest(string title, string detail)
    { 
        return new BadRequestObjectResult(new ProblemDetails
        {
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Title = title,
            Status = (int)HttpStatusCode.BadRequest,
            Detail = detail
        });
    }
}