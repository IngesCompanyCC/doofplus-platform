using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Requires a signed-in user (401 without a valid token). With roles, the user must have one of them (403).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeAttribute(params Roles[] roles) : Attribute, IAuthorizationFilter
{
    public IReadOnlyCollection<Roles> AllowedRoles { get; } = roles;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any()) return;
        if (context.HttpContext.Items["User"] is not User user)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        if (AllowedRoles.Count > 0 && !AllowedRoles.Contains(user.Role))
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
    }
}
