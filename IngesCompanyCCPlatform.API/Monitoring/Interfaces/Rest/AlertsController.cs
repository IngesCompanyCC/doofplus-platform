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
///     REST controller of the alerts raised by the sensors.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Alerts")]
public class AlertsController(
    IAlertCommandService alertCommandService,
    IAlertQueryService alertQueryService,
    IStringLocalizer<MonitoringMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Raise an alert")]
    [EndpointDescription("Raises an alert by hand; it starts as Open. The out-of-range readings raise their own alerts.")]
    [ProducesResponseType(typeof(AlertResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAlert([FromBody] CreateAlertResource resource,
        CancellationToken cancellationToken)
    {
        var command = AlertCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await alertCommandService.Handle(command, cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromCommandResult(this, result, messages,
            problemDetailsFactory,
            created => CreatedAtAction(nameof(GetAlertById), new { alertId = created.Id.Value },
                AlertResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the alerts")]
    [EndpointDescription("Gets all the alerts, or the ones of a sensor or of a production batch.")]
    [ProducesResponseType(typeof(IEnumerable<AlertResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAlerts([FromQuery] string? sensorCode, [FromQuery] string? batchCode,
        CancellationToken cancellationToken)
    {
        var alerts = !string.IsNullOrWhiteSpace(sensorCode)
            ? await alertQueryService.Handle(new GetAllAlertsBySensorCodeQuery(new SensorCode(sensorCode)),
                cancellationToken)
            : !string.IsNullOrWhiteSpace(batchCode)
                ? await alertQueryService.Handle(new GetAllAlertsByBatchCodeQuery(batchCode), cancellationToken)
                : await alertQueryService.Handle(new GetAllAlertsQuery(), cancellationToken);
        return Ok(alerts.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{alertId:int}")]
    [EndpointSummary("Get an alert by its id")]
    [EndpointDescription("Gets an alert by the provided id.")]
    [ProducesResponseType(typeof(AlertResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAlertById([FromRoute] int alertId, CancellationToken cancellationToken)
    {
        var alert = await alertQueryService.Handle(new GetAlertByIdQuery(alertId), cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromQueryResult(this, alert,
            AlertErrors.NotFoundById(alertId), MonitoringError.AlertNotFound, messages, problemDetailsFactory,
            found => Ok(AlertResourceFromEntityAssembler.ToResourceFromEntity(found)), alertId);
    }

    [HttpPut("{alertId:int}")]
    [EndpointSummary("Update the handling of an alert")]
    [EndpointDescription("Records the status, who took charge, the response and the linked record of an alert.")]
    [ProducesResponseType(typeof(AlertResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAlert([FromRoute] int alertId, [FromBody] UpdateAlertResource resource,
        CancellationToken cancellationToken)
    {
        var command = AlertCommandFromResourceAssembler.ToCommandFromResource(alertId, resource);
        var result = await alertCommandService.Handle(command, cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromCommandResult(this, result, messages,
            problemDetailsFactory, updated => Ok(AlertResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }
}
