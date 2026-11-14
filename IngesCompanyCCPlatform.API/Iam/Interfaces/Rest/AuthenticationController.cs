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
///     Sign-in with two-factor authentication and sign-up. Both endpoints are public.
/// </summary>
[ApiController]
[Route("api/v1/authentication")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Authentication")]
public class AuthenticationController(
    IUserCommandService userCommandService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [EndpointSummary("Sign in")]
    [EndpointDescription("Checks the email and the password. When the user enrolled two-factor authentication and no code " +
                         "is sent, answers 401 with detail two-factor-required; send it again with the code to receive " +
                         "the user and its JWT.")]
    [ProducesResponseType(typeof(AuthenticatedUserResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource, CancellationToken cancellationToken)
    {
        var result = await userCommandService.Handle(
            SignInCommandFromResourceAssembler.ToCommandFromResource(resource), cancellationToken);
        return IamActionResultAssembler.ToActionResult(this, result, problemDetailsFactory,
            auth => Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(auth.user, auth.token)));
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [EndpointSummary("Sign up")]
    [EndpointDescription("Registers a user with its role, workspace and organization.")]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource, CancellationToken cancellationToken)
    {
        var result = await userCommandService.Handle(
            SignUpCommandFromResourceAssembler.ToCommandFromResource(resource), cancellationToken);
        return IamActionResultAssembler.ToActionResult(this, result, problemDetailsFactory,
            user => StatusCode(StatusCodes.Status201Created, UserResourceFromEntityAssembler.ToResourceFromEntity(user)));
    }
}
