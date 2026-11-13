using System.Net.Mime;
using IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Manufacturing.Resources;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest;

/// <summary>
///     REST controller of the lots of raw material: receipt, inspection, release and allocation to a batch.
/// </summary>
[ApiController]
[Route("api/v1/material-lots")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Material lots")]
public class RawMaterialLotsController(
    IRawMaterialLotCommandService rawMaterialLotCommandService,
    IRawMaterialLotQueryService rawMaterialLotQueryService,
    IStringLocalizer<ManufacturingMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Receive a lot")]
    [EndpointDescription("Receives a lot of raw material; it starts in Inspection with its samples requested.")]
    [ProducesResponseType(typeof(RawMaterialLotResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ReceiveRawMaterialLot([FromBody] ReceiveRawMaterialLotResource resource, CancellationToken cancellationToken)
    {
        var result = await rawMaterialLotCommandService.Handle(CatalogCommandFromResourceAssembler.ToCommandFromResource(resource), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetRawMaterialLotById), new { rawMaterialLotId = created.Id.Value }, CatalogResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get all lots")]
    [EndpointDescription("Gets all the lots of raw material.")]
    [ProducesResponseType(typeof(IEnumerable<RawMaterialLotResource>), StatusCodes.Status200OK)]

    public async Task<IActionResult> GetAllRawMaterialLots(CancellationToken cancellationToken)
    {
        var lots = await rawMaterialLotQueryService.Handle(new GetAllRawMaterialLotsQuery(), cancellationToken);
        return Ok(lots.Select(CatalogResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{rawMaterialLotId:int}")]
    [EndpointSummary("Get a lot by its id")]
    [EndpointDescription("Gets a lot of raw material by the provided id.")]
    [ProducesResponseType(typeof(RawMaterialLotResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRawMaterialLotById([FromRoute] int rawMaterialLotId, CancellationToken cancellationToken)
    {
        var entity = await rawMaterialLotQueryService.Handle(new GetRawMaterialLotByIdQuery(rawMaterialLotId), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromQueryResult(this, entity, RawMaterialLotErrors.NotFoundById(rawMaterialLotId),
            ManufacturingError.RawMaterialLotNotFound, messages, problemDetailsFactory, found => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(found)), rawMaterialLotId);
    }

    [HttpPut("{rawMaterialLotId:int}/inspection")]
    [EndpointSummary("Record the inspection of a lot")]
    [EndpointDescription("Records whether the packaging and identity were verified and the samples taken; only while the lot is in Inspection.")]
    [ProducesResponseType(typeof(RawMaterialLotResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> InspectRawMaterialLot([FromRoute] int rawMaterialLotId, [FromBody] InspectRawMaterialLotResource resource, CancellationToken cancellationToken)
    {
        var result = await rawMaterialLotCommandService.Handle(CatalogCommandFromResourceAssembler.ToCommandFromResource(rawMaterialLotId, resource), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            inspected => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(inspected)));
    }

    [HttpPost("{rawMaterialLotId:int}/approval")]
    [EndpointSummary("Release a lot")]
    [EndpointDescription("Releases a lot in Inspection once its packaging and identity are verified and its samples are taken.")]
    [ProducesResponseType(typeof(RawMaterialLotResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ApproveRawMaterialLot([FromRoute] int rawMaterialLotId, CancellationToken cancellationToken)
    {
        var result = await rawMaterialLotCommandService.Handle(new ApproveRawMaterialLotCommand(rawMaterialLotId), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            changed => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(changed)));
    }

    [HttpPost("{rawMaterialLotId:int}/rejection")]
    [EndpointSummary("Reject a lot")]
    [EndpointDescription("Rejects a lot in Inspection.")]
    [ProducesResponseType(typeof(RawMaterialLotResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RejectRawMaterialLot([FromRoute] int rawMaterialLotId, CancellationToken cancellationToken)
    {
        var result = await rawMaterialLotCommandService.Handle(new RejectRawMaterialLotCommand(rawMaterialLotId), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            changed => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(changed)));
    }

    [HttpPost("{rawMaterialLotId:int}/allocation")]
    [EndpointSummary("Allocate a lot to a batch")]
    [EndpointDescription("Allocates an approved lot to a production batch.")]
    [ProducesResponseType(typeof(RawMaterialLotResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AllocateRawMaterialLot([FromRoute] int rawMaterialLotId, [FromBody] AllocateRawMaterialLotResource resource, CancellationToken cancellationToken)
    {
        var result = await rawMaterialLotCommandService.Handle(new AllocateRawMaterialLotCommand(rawMaterialLotId, resource.BatchCode), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            changed => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(changed)));
    }
}
