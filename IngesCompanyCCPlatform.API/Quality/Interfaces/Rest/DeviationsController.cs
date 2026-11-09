using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Filters;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Deviations")]
[QualityInvalidDataFilter]
public class DeviationsController(
    IDeviationCommandService deviationCommandService,
    IDeviationQueryService deviationQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get the deviations")]
    [EndpointDescription("Gets all the deviations, or the ones of a production batch with ?productionBatchId=.")]
    [ProducesResponseType(typeof(IEnumerable<DeviationResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDeviations([FromQuery] int? productionBatchId,
        CancellationToken cancellationToken)
    {
        var deviations = productionBatchId is null
            ? await deviationQueryService.Handle(new GetAllDeviationsQuery(), cancellationToken)
            : await deviationQueryService.Handle(
                new GetDeviationsByProductionBatchIdQuery(new ProductionBatchId(productionBatchId.Value)),
                cancellationToken);
        return Ok(deviations.Select(DeviationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{deviationId:int}")]
    [EndpointSummary("Get a deviation by id")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDeviationById(int deviationId, CancellationToken cancellationToken)
    {
        var deviation = await deviationQueryService.Handle(new GetDeviationByIdQuery(deviationId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQuery(this, deviation, problemDetailsFactory,
            QualityError.DeviationNotFound, QualityErrors.DeviationNotFound(deviationId).Message,
            found => Ok(DeviationResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [EndpointSummary("Register a deviation")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterDeviation([FromBody] RegisterDeviationResource resource,
        CancellationToken cancellationToken)
    {
        var command = DeviationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await deviationCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetDeviationById), new { deviationId = created.Id.Value },
                DeviationResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPost("{deviationId:int}/classification")]
    [EndpointSummary("Change the severity of a deviation")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ClassifyDeviation(int deviationId, [FromBody] ClassifyDeviationResource resource,
        CancellationToken cancellationToken)
    {
        var command = DeviationCommandFromResourceAssembler.ToCommandFromResource(deviationId, resource);
        return ToOk(await deviationCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{deviationId:int}/investigation")]
    [EndpointSummary("Start the investigation of a deviation")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> StartDeviationInvestigation(int deviationId, CancellationToken cancellationToken)
    {
        return ToOk(await deviationCommandService.Handle(new StartDeviationInvestigationCommand(deviationId),
            cancellationToken));
    }

    [HttpPost("{deviationId:int}/root-cause")]
    [EndpointSummary("Register the root cause of a deviation")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterRootCause(int deviationId, [FromBody] RegisterRootCauseResource resource,
        CancellationToken cancellationToken)
    {
        var command = DeviationCommandFromResourceAssembler.ToCommandFromResource(deviationId, resource);
        return ToOk(await deviationCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{deviationId:int}/capa-actions")]
    [EndpointSummary("Add a CAPA action to a deviation")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddCapaAction(int deviationId, [FromBody] AddCapaActionResource resource,
        CancellationToken cancellationToken)
    {
        var command = DeviationCommandFromResourceAssembler.ToCommandFromResource(deviationId, resource);
        return ToOk(await deviationCommandService.Handle(command, cancellationToken));
    }

    [HttpPatch("{deviationId:int}/capa-actions/{capaActionId:int}/progress")]
    [EndpointSummary("Update the progress of a CAPA action")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCapaActionProgress(int deviationId, int capaActionId,
        [FromBody] UpdateCapaActionProgressResource resource, CancellationToken cancellationToken)
    {
        var command = DeviationCommandFromResourceAssembler.ToCommandFromResource(deviationId, capaActionId, resource);
        return ToOk(await deviationCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{deviationId:int}/capa-actions/{capaActionId:int}/verification")]
    [EndpointSummary("Verify an implemented CAPA action")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> VerifyCapaAction(int deviationId, int capaActionId,
        CancellationToken cancellationToken)
    {
        return ToOk(await deviationCommandService.Handle(new VerifyCapaActionCommand(deviationId, capaActionId),
            cancellationToken));
    }

    [HttpPost("{deviationId:int}/closure")]
    [EndpointSummary("Close a deviation")]
    [EndpointDescription("Requires a root cause and at least one CAPA action, all of them verified.")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CloseDeviation(int deviationId, CancellationToken cancellationToken)
    {
        return ToOk(await deviationCommandService.Handle(new CloseDeviationCommand(deviationId), cancellationToken));
    }

    private IActionResult ToOk(Result<Deviation> result)
    {
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            deviation => Ok(DeviationResourceFromEntityAssembler.ToResourceFromEntity(deviation)));
    }
}
