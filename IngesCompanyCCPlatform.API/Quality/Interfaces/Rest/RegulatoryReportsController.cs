using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Filters;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Regulatory Reports")]
[QualityInvalidDataFilter]
public class RegulatoryReportsController(
    IRegulatoryReportCommandService regulatoryReportCommandService,
    IRegulatoryReportQueryService regulatoryReportQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get the regulatory reports of an organization")]
    [ProducesResponseType(typeof(IEnumerable<RegulatoryReportResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRegulatoryReportsByOrganizationId([FromQuery] int organizationId,
        CancellationToken cancellationToken)
    {
        var reports = await regulatoryReportQueryService.Handle(
            new GetRegulatoryReportsByOrganizationIdQuery(new OrganizationId(organizationId)), cancellationToken);
        return Ok(reports.Select(RegulatoryReportResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{regulatoryReportId:int}")]
    [EndpointSummary("Get a regulatory report by id")]
    [ProducesResponseType(typeof(RegulatoryReportResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRegulatoryReportById(int regulatoryReportId,
        CancellationToken cancellationToken)
    {
        var report = await regulatoryReportQueryService.Handle(new GetRegulatoryReportByIdQuery(regulatoryReportId),
            cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQuery(this, report, problemDetailsFactory,
            QualityError.RegulatoryReportNotFound, $"Regulatory report not found for id {regulatoryReportId}.",
            found => Ok(RegulatoryReportResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [EndpointSummary("Register a regulatory report")]
    [ProducesResponseType(typeof(RegulatoryReportResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterRegulatoryReport([FromBody] RegisterRegulatoryReportResource resource,
        CancellationToken cancellationToken)
    {
        var command = RegisterRegulatoryReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await regulatoryReportCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetRegulatoryReportById), new { regulatoryReportId = created.Id.Value },
                RegulatoryReportResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }
}
