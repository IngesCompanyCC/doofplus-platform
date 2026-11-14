using System.Net.Mime;
using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest;

/// <summary>
///     Facilities (plants) of the organizations, as the Web App reads and creates them.
/// </summary>
[ApiController]
[Route("api/v1/facilities")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Facilities")]
public class FacilitiesController(
    IOrganizationCommandService commandService,
    IFacilityQueryService queryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Add a facility")]
    [EndpointDescription("Adds a manufacturing plant or a QC laboratory to an organization and records it in the administrative activity.")]
    [ProducesResponseType(typeof(FacilityResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddFacility([FromBody] AddFacilityResource resource, CancellationToken cancellationToken)
    {
        var type = resource.Type?.Replace("-", string.Empty).Replace("_", string.Empty);
        if (!Enum.TryParse<FacilityType>(type, true, out var facilityType) || !Enum.IsDefined(facilityType))
            return problemDetailsFactory.CreateProblemDetails(this, StatusCodes.Status400BadRequest,
                OrganizationsError.InvalidOperation, "type must be Manufacturing or QcLaboratory.");

        var command = new AddFacilityCommand(resource.OrganizationId, resource.Name, facilityType, resource.Timezone, resource.OwnerName);
        var result = await commandService.Handle(command, cancellationToken);

        return OrganizationsActionResultAssembler.ToActionResult(
            this,
            result,
            problemDetailsFactory,
            facility => StatusCode(StatusCodes.Status201Created, FacilityResourceFromEntityAssembler.ToResourceFromEntity(facility)));
    }

    [HttpGet]
    [EndpointSummary("Get facilities")]
    [EndpointDescription("Gets every facility, or only the ones of an organization when organizationId is given.")]
    [ProducesResponseType(typeof(IEnumerable<FacilityResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllFacilities([FromQuery] int? organizationId, CancellationToken cancellationToken)
    {
        var facilities = await queryService.Handle(new GetAllFacilitiesQuery(organizationId), cancellationToken);
        return Ok(facilities.Select(FacilityResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
