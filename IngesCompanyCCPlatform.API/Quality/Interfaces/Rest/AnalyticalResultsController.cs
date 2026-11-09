using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
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
[Tags("Analytical Results")]
[QualityInvalidDataFilter]
public class AnalyticalResultsController(
    IAnalyticalResultCommandService analyticalResultCommandService,
    IAnalyticalResultQueryService analyticalResultQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get the analytical results of a production batch")]
    [ProducesResponseType(typeof(IEnumerable<AnalyticalResultResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAnalyticalResultsByProductionBatchId([FromQuery] int productionBatchId,
        CancellationToken cancellationToken)
    {
        var results = await analyticalResultQueryService.Handle(
            new GetAnalyticalResultsByProductionBatchIdQuery(new ProductionBatchId(productionBatchId)),
            cancellationToken);
        return Ok(results.Select(AnalyticalResultResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{analyticalResultId:int}")]
    [EndpointSummary("Get an analytical result by id")]
    [ProducesResponseType(typeof(AnalyticalResultResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAnalyticalResultById(int analyticalResultId,
        CancellationToken cancellationToken)
    {
        var result = await analyticalResultQueryService.Handle(new GetAnalyticalResultByIdQuery(analyticalResultId),
            cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQuery(this, result, problemDetailsFactory,
            QualityError.AnalyticalResultNotFound, QualityErrors.AnalyticalResultNotFound(analyticalResultId).Message,
            found => Ok(AnalyticalResultResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [EndpointSummary("Register an analytical result")]
    [EndpointDescription("An out-of-specification (OOS) result also registers a major deviation for the batch.")]
    [ProducesResponseType(typeof(AnalyticalResultResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAnalyticalResult([FromBody] RegisterAnalyticalResultResource resource,
        CancellationToken cancellationToken)
    {
        var command = RegisterAnalyticalResultCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await analyticalResultCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetAnalyticalResultById), new { analyticalResultId = created.Id.Value },
                AnalyticalResultResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }
}
