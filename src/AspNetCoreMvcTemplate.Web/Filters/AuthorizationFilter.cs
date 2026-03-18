using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AspNetCoreMvcTemplate.Web.Helpers.Constants;

namespace AspNetCoreMvcTemplate.Web.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizationFilter : Attribute, IAuthorizationFilter
{
    private readonly string[] _requiredRoles;

    public AuthorizationFilter(params string[] requiredRoles)
    {
        _requiredRoles = requiredRoles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new UnauthorizedObjectResult(new { error = ErrorMessages.UNAUTHORIZED });
            return;
        }

        if (_requiredRoles.Length > 0 &&
            !_requiredRoles.Any(role => context.HttpContext.User.IsInRole(role)))
        {
            context.Result = new ForbidResult();
        }
    }
}
