using System.Net.Mime;
using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest;

[ApiController]
[Route("api/v1/user-profiles")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("User Profiles")]
public class UserProfilesController(
    IUserProfileCommandService userProfileCommandService,
    IUserProfileQueryService userProfileQueryService) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create user profile")]
    [ProducesResponseType(typeof(UserProfileResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserProfile(
        [FromBody] CreateUserProfileResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateUserProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userProfileCommandService.Handle(command, cancellationToken);

        return result switch
        {
            Result<Domain.Model.Aggregates.UserProfile, string>.Success success =>
                CreatedAtAction(
                    nameof(GetUserProfileById),
                    new { profileId = success.Value.Id },
                    UserProfileResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Domain.Model.Aggregates.UserProfile, string>.Failure failure =>
                BadRequest(new { message = failure.Error }),

            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpGet("{profileId:guid}")]
    [EndpointSummary("Get user profile by id")]
    [ProducesResponseType(typeof(UserProfileResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserProfileById(
        Guid profileId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProfileByIdQuery(profileId);
        var profile = await userProfileQueryService.Handle(query, cancellationToken);

        if (profile is null)
            return NotFound(new { message = "User profile was not found." });

        return Ok(UserProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    [HttpGet]
    [EndpointSummary("Get user profiles by organization id")]
    [ProducesResponseType(typeof(IEnumerable<UserProfileResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProfilesByOrganizationId(
        [FromQuery] Guid organizationId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProfilesByOrganizationIdQuery(organizationId);
        var profiles = await userProfileQueryService.Handle(query, cancellationToken);

        var resources = profiles.Select(UserProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}