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
[Route("api/v1/organizations")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Organizations")]
public class OrganizationsController(
    IOrganizationCommandService organizationCommandService,
    IOrganizationQueryService organizationQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Register organization")]
    [ProducesResponseType(typeof(OrganizationResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterOrganization(
        [FromBody] RegisterOrganizationResource resource,
        CancellationToken cancellationToken)
    {
        var command = RegisterOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await organizationCommandService.Handle(command, cancellationToken);

        return OrganizationsActionResultAssembler.ToActionResult(
            this,
            result,
            problemDetailsFactory,
            organization => CreatedAtAction(
                nameof(GetOrganizationById),
                new { organizationId = organization.Id },
                OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization)));
    }

    [HttpGet("{organizationId:int}")]
    [EndpointSummary("Get organization by id")]
    [ProducesResponseType(typeof(OrganizationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrganizationById(
        int organizationId,
        CancellationToken cancellationToken)
    {
        var query = new GetOrganizationByIdQuery(organizationId);
        var organization = await organizationQueryService.Handle(query, cancellationToken);

        if (organization is null)
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status404NotFound,
                OrganizationsError.OrganizationNotFound,
                $"Organization with ID {organizationId} was not found.");

        return Ok(OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization));
    }
}
