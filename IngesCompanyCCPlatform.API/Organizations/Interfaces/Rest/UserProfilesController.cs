using System.Net.Mime;
using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest;

[ApiController]
[Route("api/v1/user-profiles")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("User Profiles")]
public class UserProfilesController(
    IUserProfileCommandService userProfileCommandService,
    IUserProfileQueryService userProfileQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create user profile")]
    [ProducesResponseType(typeof(UserProfileResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUserProfile(
        [FromBody] CreateUserProfileResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateUserProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userProfileCommandService.Handle(command, cancellationToken);

        return OrganizationsActionResultAssembler.ToActionResult(
            this,
            result,
            problemDetailsFactory,
            profile => CreatedAtAction(
                nameof(GetUserProfileById),
                new { profileId = profile.Id },
                UserProfileResourceFromEntityAssembler.ToResourceFromEntity(profile)));
    }

    [HttpGet("{profileId:int}")]
    [EndpointSummary("Get user profile by id")]
    [ProducesResponseType(typeof(UserProfileResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserProfileById(
        int profileId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProfileByIdQuery(profileId);
        var profile = await userProfileQueryService.Handle(query, cancellationToken);

        if (profile is null)
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status404NotFound,
                OrganizationsError.UserProfileNotFound,
                $"User profile with ID {profileId} was not found.");

        return Ok(UserProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    [HttpGet]
    [EndpointSummary("Get user profiles by organization id")]
    [ProducesResponseType(typeof(IEnumerable<UserProfileResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProfilesByOrganizationId(
        [FromQuery] int organizationId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProfilesByOrganizationIdQuery(organizationId);
        var profiles = await userProfileQueryService.Handle(query, cancellationToken);

        var resources = profiles.Select(UserProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
