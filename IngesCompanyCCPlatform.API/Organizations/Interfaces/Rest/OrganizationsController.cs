using System.Net.Mime;
using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest;

[ApiController]
[Authorize]
[Route("api/v1/organizations")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Organizations")]
public class OrganizationsController(
    IOrganizationCommandService organizationCommandService,
    IOrganizationQueryService organizationQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpGet]
    [EndpointSummary("Get every organization, or the one registered with a RUC")]
    [EndpointDescription("Without a token it only answers the search by RUC, which the registration of a new organization uses; listing every organization needs a token.")]
    [ProducesResponseType(typeof(IEnumerable<OrganizationResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllOrganizations(
        [FromQuery] string? ruc,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(ruc))
        {
            if (HttpContext.Items["User"] is null) return Unauthorized();

            var organizations = await organizationQueryService.Handle(new GetAllOrganizationsQuery(), cancellationToken);
            return Ok(organizations.Select(OrganizationResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var organization = await organizationQueryService.Handle(new GetOrganizationByRucQuery(ruc), cancellationToken);
        return Ok(organization is null
            ? []
            : new[] { OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization) });
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
