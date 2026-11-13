using System.Net.Mime;
using IngesCompanyCCPlatform.API.Monitoring.Application.CommandServices;
using IngesCompanyCCPlatform.API.Monitoring.Application.QueryServices;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Monitoring.Resources;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest;

/// <summary>
///     REST controller of the telemetry readings.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Readings")]
public class ReadingsController(
    IReadingCommandService readingCommandService,
    IReadingQueryService readingQueryService,
    IStringLocalizer<MonitoringMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Record a reading")]
    [EndpointDescription("Records a reading of a sensor by hand; a value outside its range raises an alert.")]
    [ProducesResponseType(typeof(ReadingResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateReading([FromBody] CreateReadingResource resource,
        CancellationToken cancellationToken)
    {
        var command = ReadingCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await readingCommandService.Handle(command, cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromCommandResult(this, result, messages,
            problemDetailsFactory,
            created => CreatedAtAction(nameof(GetReadingById), new { readingId = created.Id.Value },
                ReadingResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the readings")]
    [EndpointDescription("Gets all the readings, or the ones of a sensor or of a production batch, the newest first.")]
    [ProducesResponseType(typeof(IEnumerable<ReadingResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetReadings([FromQuery] string? sensorCode, [FromQuery] string? batchCode,
        CancellationToken cancellationToken)
    {
        var readings = !string.IsNullOrWhiteSpace(sensorCode)
            ? await readingQueryService.Handle(new GetAllReadingsBySensorCodeQuery(new SensorCode(sensorCode)),
                cancellationToken)
            : !string.IsNullOrWhiteSpace(batchCode)
                ? await readingQueryService.Handle(new GetAllReadingsByBatchCodeQuery(batchCode), cancellationToken)
                : await readingQueryService.Handle(new GetAllReadingsQuery(), cancellationToken);
        return Ok(readings.Select(ReadingResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{readingId:int}")]
    [EndpointSummary("Get a reading by its id")]
    [EndpointDescription("Gets a reading by the provided id.")]
    [ProducesResponseType(typeof(ReadingResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReadingById([FromRoute] int readingId, CancellationToken cancellationToken)
    {
        var reading = await readingQueryService.Handle(new GetReadingByIdQuery(readingId), cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromQueryResult(this, reading,
            ReadingErrors.NotFoundById(readingId), MonitoringError.ReadingNotFound, messages, problemDetailsFactory,
            found => Ok(ReadingResourceFromEntityAssembler.ToResourceFromEntity(found)), readingId);
    }
}
