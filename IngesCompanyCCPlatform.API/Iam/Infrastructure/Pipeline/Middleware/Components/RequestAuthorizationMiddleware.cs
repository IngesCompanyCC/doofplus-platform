using IngesCompanyCCPlatform.API.Iam.Application.Internal.OutboundServices;
using IngesCompanyCCPlatform.API.Iam.Application.QueryServices;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
///     Reads the Bearer token of the request and, when it is valid, puts its user in HttpContext.Items["User"].
///     The [Authorize] attribute then decides 401 or 403; endpoints without it stay open.
/// </summary>
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUserQueryService userQueryService, ITokenService tokenService)
    {
        var allowAnonymous = context.GetEndpoint()?.Metadata.Any(m => m is AllowAnonymousAttribute) ?? false;
        if (!allowAnonymous)
        {
            var header = context.Request.Headers.Authorization.FirstOrDefault();
            if (header is not null && header.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var userId = await tokenService.ValidateToken(header["Bearer ".Length..].Trim());
                if (userId is not null)
                    context.Items["User"] =
                        await userQueryService.Handle(new GetUserByIdQuery(userId.Value), context.RequestAborted);
            }
        }
        await next(context);
    }
}
