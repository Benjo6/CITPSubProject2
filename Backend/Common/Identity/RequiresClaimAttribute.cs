using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Identity;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresClaimAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _claimName;
    private readonly string _claimValue;

    public RequiresClaimAttribute(string claimValue, string claimName)
    {
        _claimValue = claimValue;
        _claimName = claimName;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.HasClaim(_claimName, _claimValue))
        {
            context.Result = new ForbidResult();
        }
    }
}
