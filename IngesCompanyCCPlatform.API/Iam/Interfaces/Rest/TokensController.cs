using System.Net.Mime;
using IngesCompanyCCPlatform.API.Iam.Application.CommandServices;
using IngesCompanyCCPlatform.API.Iam.Application.QueryServices;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest;

/// <summary>
///     Sign-in in the style of the professor's platform: POST /api/v1/tokens.
/// </summary>
[ApiController]
[Authorize]
[Route("api/v1/tokens")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Tokens")]
public class TokensController(
    IUserCommandService userCommandService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    [EndpointSummary("Create authentication token (sign-in)")]
    [ProducesResponseType(typeof(AuthenticatedUserResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateToken([FromBody] SignInResource resource, CancellationToken cancellationToken)
    {
        var result = await userCommandService.Handle(
            SignInCommandFromResourceAssembler.ToCommandFromResource(resource), cancellationToken);
        return IamActionResultAssembler.ToActionResult(this, result, problemDetailsFactory,
            auth => Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(auth.user, auth.token)));
    }
}
