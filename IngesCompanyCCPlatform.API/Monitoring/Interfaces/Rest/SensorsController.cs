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
///     REST controller of the sensors and their ThingsBoard devices.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Sensors")]
public class SensorsController(
    ISensorCommandService sensorCommandService,
    ISensorQueryService sensorQueryService,
    IStringLocalizer<MonitoringMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Register a sensor")]
    [EndpointDescription("Registers a sensor on an existing piece of equipment, with its device and accepted range.")]
    [ProducesResponseType(typeof(SensorResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateSensor([FromBody] CreateSensorResource resource,
        CancellationToken cancellationToken)
    {
        var command = SensorCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await sensorCommandService.Handle(command, cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromCommandResult(this, result, messages,
            problemDetailsFactory,
            created => CreatedAtAction(nameof(GetSensorById), new { sensorId = created.Id.Value },
                SensorResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the sensors")]
    [EndpointDescription("Gets all the sensors, or the ones installed on the equipment with the given code.")]
    [ProducesResponseType(typeof(IEnumerable<SensorResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSensors([FromQuery] string? equipmentCode, CancellationToken cancellationToken)
    {
        var sensors = string.IsNullOrWhiteSpace(equipmentCode)
            ? await sensorQueryService.Handle(new GetAllSensorsQuery(), cancellationToken)
            : await sensorQueryService.Handle(new GetAllSensorsByEquipmentCodeQuery(new EquipmentCode(equipmentCode)),
                cancellationToken);
        return Ok(sensors.Select(SensorResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{sensorId:int}")]
    [EndpointSummary("Get a sensor by its id")]
    [EndpointDescription("Gets a sensor by the provided id.")]
    [ProducesResponseType(typeof(SensorResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSensorById([FromRoute] int sensorId, CancellationToken cancellationToken)
    {
        var sensor = await sensorQueryService.Handle(new GetSensorByIdQuery(sensorId), cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromQueryResult(this, sensor,
            SensorErrors.NotFoundById(sensorId), MonitoringError.SensorNotFound, messages, problemDetailsFactory,
            found => Ok(SensorResourceFromEntityAssembler.ToResourceFromEntity(found)), sensorId);
    }

    [HttpPut("{sensorId:int}")]
    [EndpointSummary("Update a sensor")]
    [EndpointDescription("Updates the type, range, status and assigned batch of a sensor; its code and device do not change.")]
    [ProducesResponseType(typeof(SensorResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSensor([FromRoute] int sensorId, [FromBody] UpdateSensorResource resource,
        CancellationToken cancellationToken)
    {
        var command = SensorCommandFromResourceAssembler.ToCommandFromResource(sensorId, resource);
        var result = await sensorCommandService.Handle(command, cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromCommandResult(this, result, messages,
            problemDetailsFactory, updated => Ok(SensorResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }
}
