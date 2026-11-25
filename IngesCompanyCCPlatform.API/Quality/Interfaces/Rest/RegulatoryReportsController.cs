using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Quality.Resources;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

/// <summary>
///     REST controller of the regulatory reports of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Regulatory Reports")]
public class RegulatoryReportsController(
    IRegulatoryReportCommandService regulatoryReportCommandService,
    IRegulatoryReportQueryService regulatoryReportQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a regulatory report")]
    [EndpointDescription("Creates a regulatory report.")]
    [ProducesResponseType(typeof(RegulatoryReportResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateRegulatoryReport([FromBody] CreateRegulatoryReportResource resource, CancellationToken cancellationToken)
    {
        var command = RegulatoryReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await regulatoryReportCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetRegulatoryReportById), new { regulatoryReportId = created.Id.Value },
                RegulatoryReportResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the regulatory reports")]
    [EndpointDescription("Gets the regulatory reports.")]
    [ProducesResponseType(typeof(IEnumerable<RegulatoryReportResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRegulatoryReports(CancellationToken cancellationToken)
    {
        var items = await regulatoryReportQueryService.Handle(new GetAllRegulatoryReportsQuery(), cancellationToken);
        return Ok(items.Select(RegulatoryReportResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{regulatoryReportId:int}")]
    [EndpointSummary("Get a regulatory report by its id")]
    [EndpointDescription("Gets a regulatory report by the provided id.")]
    [ProducesResponseType(typeof(RegulatoryReportResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRegulatoryReportById([FromRoute] int regulatoryReportId, CancellationToken cancellationToken)
    {
        var item = await regulatoryReportQueryService.Handle(new GetRegulatoryReportByIdQuery(regulatoryReportId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, RegulatoryReportErrors.NotFoundById(regulatoryReportId),
            QualityError.RegulatoryReportNotFound, messages, problemDetailsFactory,
            found => Ok(RegulatoryReportResourceFromEntityAssembler.ToResourceFromEntity(found)), regulatoryReportId);
    }
}
