using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AspNetCoreMvcTemplate.Web.Helpers.Constants;

namespace AspNetCoreMvcTemplate.Web.Filters;

public class ValidateModelFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(e => e.Value?.Errors.Count > 0)
                .ToDictionary(
                    e => e.Key,
                    e => e.Value!.Errors.Select(x => x.ErrorMessage).ToArray());

            context.Result = new BadRequestObjectResult(new
            {
                error = ErrorMessages.VALIDATION_FAILED,
                details = errors
            });
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
