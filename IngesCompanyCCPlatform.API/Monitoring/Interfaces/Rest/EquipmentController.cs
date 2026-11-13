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
///     REST controller of the equipment of the plant (TS07).
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Equipment")]
public class EquipmentController(
    IEquipmentCommandService equipmentCommandService,
    IEquipmentQueryService equipmentQueryService,
    IStringLocalizer<MonitoringMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Register a piece of equipment")]
    [EndpointDescription("Registers a piece of equipment with its calibration and maintenance (TS07, scenario 1).")]
    [ProducesResponseType(typeof(EquipmentResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentResource resource,
        CancellationToken cancellationToken)
    {
        var command = EquipmentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await equipmentCommandService.Handle(command, cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromCommandResult(this, result, messages,
            problemDetailsFactory,
            created => CreatedAtAction(nameof(GetEquipmentById), new { equipmentId = created.Id.Value },
                EquipmentResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the equipment")]
    [EndpointDescription("Gets all the equipment, or the one with the given code.")]
    [ProducesResponseType(typeof(IEnumerable<EquipmentResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEquipment([FromQuery] string? code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            var equipment = await equipmentQueryService.Handle(new GetAllEquipmentQuery(), cancellationToken);
            return Ok(equipment.Select(EquipmentResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var found = await equipmentQueryService.Handle(new GetEquipmentByCodeQuery(new EquipmentCode(code)),
            cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromQueryResult(this, found,
            EquipmentErrors.NotFoundByCode(code), MonitoringError.EquipmentNotFound, messages, problemDetailsFactory,
            equipment => Ok(new[] { EquipmentResourceFromEntityAssembler.ToResourceFromEntity(equipment) }), code);
    }

    [HttpGet("{equipmentId:int}")]
    [EndpointSummary("Get a piece of equipment by its id")]
    [EndpointDescription("Gets a piece of equipment by the provided id.")]
    [ProducesResponseType(typeof(EquipmentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEquipmentById([FromRoute] int equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await equipmentQueryService.Handle(new GetEquipmentByIdQuery(equipmentId), cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromQueryResult(this, equipment,
            EquipmentErrors.NotFoundById(equipmentId), MonitoringError.EquipmentNotFound, messages,
            problemDetailsFactory, found => Ok(EquipmentResourceFromEntityAssembler.ToResourceFromEntity(found)),
            equipmentId);
    }

    [HttpPut("{equipmentId:int}")]
    [EndpointSummary("Update a piece of equipment")]
    [EndpointDescription("Updates the data, calibration and maintenance of a piece of equipment; its code does not change.")]
    [ProducesResponseType(typeof(EquipmentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEquipment([FromRoute] int equipmentId,
        [FromBody] UpdateEquipmentResource resource, CancellationToken cancellationToken)
    {
        var command = EquipmentCommandFromResourceAssembler.ToCommandFromResource(equipmentId, resource);
        var result = await equipmentCommandService.Handle(command, cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromCommandResult(this, result, messages,
            problemDetailsFactory, updated => Ok(EquipmentResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }

    [HttpDelete("{equipmentId:int}")]
    [EndpointSummary("Remove a piece of equipment")]
    [EndpointDescription("Removes a piece of equipment; 409 when one of its sensors is monitoring a batch (TS07, scenario 2).")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteEquipment([FromRoute] int equipmentId, CancellationToken cancellationToken)
    {
        var result = await equipmentCommandService.Handle(new DeleteEquipmentCommand(equipmentId), cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromCommandResult(this, result, messages,
            problemDetailsFactory, _ => NoContent());
    }
}
