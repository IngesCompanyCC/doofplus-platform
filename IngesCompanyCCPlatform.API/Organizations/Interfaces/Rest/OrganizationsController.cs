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
[Route("api/v1/organizations")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Organizations")]
public class OrganizationsController(
    IOrganizationCommandService organizationCommandService,
    IOrganizationQueryService organizationQueryService) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Register organization")]
    [ProducesResponseType(typeof(OrganizationResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterOrganization(
        [FromBody] RegisterOrganizationResource resource,
        CancellationToken cancellationToken)
    {
        var command = RegisterOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await organizationCommandService.Handle(command, cancellationToken);

        return result switch
        {
            Result<Domain.Model.Aggregates.Organization, string>.Success success =>
                CreatedAtAction(
                    nameof(GetOrganizationById),
                    new { organizationId = success.Value.Id },
                    OrganizationResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Domain.Model.Aggregates.Organization, string>.Failure failure =>
                BadRequest(new { message = failure.Error }),

            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpGet("{organizationId:guid}")]
    [EndpointSummary("Get organization by id")]
    [ProducesResponseType(typeof(OrganizationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrganizationById(
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        var query = new GetOrganizationByIdQuery(organizationId);
        var organization = await organizationQueryService.Handle(query, cancellationToken);

        if (organization is null)
            return NotFound(new { message = "Organization was not found." });

        return Ok(OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization));
    }
}