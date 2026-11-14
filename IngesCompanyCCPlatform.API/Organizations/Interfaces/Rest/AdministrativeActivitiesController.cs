using System.Net.Mime;
using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest;

/// <summary>
///     Recent administrative activity shown in the Administration overview of the Web App.
/// </summary>
[ApiController]
[Route("api/v1/administrative-activities")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Administrative Activities")]
public class AdministrativeActivitiesController(IAdministrativeActivityQueryService queryService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get the administrative activities")]
    [EndpointDescription("Gets the administrative activities, the most recent first, optionally only the ones of an organization.")]
    [ProducesResponseType(typeof(IEnumerable<AdministrativeActivityResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAdministrativeActivities([FromQuery] int? organizationId, CancellationToken cancellationToken)
    {
        var activities = await queryService.Handle(new GetAllAdministrativeActivitiesQuery(organizationId), cancellationToken);
        return Ok(activities.Select(AdministrativeActivityResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
