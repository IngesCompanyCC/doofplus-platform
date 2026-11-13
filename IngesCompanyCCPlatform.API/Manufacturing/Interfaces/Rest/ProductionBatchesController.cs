using System.Net.Mime;
using IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest;

/// <summary>
///     REST controller exposing endpoints for pharmaceutical production batches.
/// </summary>
[ApiController]
[Route("api/v1/production-batches")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Production Batches")]
public class ProductionBatchesController(
    IProductionBatchCommandService commandService,
    IProductionBatchQueryService queryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a new production batch")]
    [EndpointDescription("Creates a new pharmaceutical production batch in PLANNED status.")]
    [ProducesResponseType(typeof(ProductionBatchResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateBatch(
        [FromBody] CreateBatchResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateBatchCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return ManufacturingActionResultAssembler.ToActionResult(
            this,
            result,
            problemDetailsFactory,
            batch => CreatedAtAction(
                nameof(GetBatchById),
                new { id = batch.Id },
                ProductionBatchResourceFromEntityAssembler.ToResourceFromEntity(batch)));
    }

    [HttpGet]
    [EndpointSummary("Get all production batches")]
    [EndpointDescription("Retrieves all production batches registered in the manufacturing system.")]
    [ProducesResponseType(typeof(IEnumerable<ProductionBatchResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBatches(CancellationToken cancellationToken)
    {
        var batches = await queryService.Handle(new GetAllBatchesQuery(), cancellationToken);
        var resources = batches.Select(ProductionBatchResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [EndpointSummary("Get production batch by ID")]
    [EndpointDescription("Retrieves the details of a single production batch by its database identifier.")]
    [ProducesResponseType(typeof(ProductionBatchResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBatchById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var batch = await queryService.Handle(new GetBatchByIdQuery(id), cancellationToken);

        if (batch is null)
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status404NotFound,
                ManufacturingError.BatchNotFound,
                $"Production batch with ID {id} was not found.");

        return Ok(ProductionBatchResourceFromEntityAssembler.ToResourceFromEntity(batch));
    }

    [HttpGet("by-number/{batchNumber}")]
    [EndpointSummary("Get production batch by batch number")]
    [EndpointDescription("Retrieves a production batch by its unique business batch number (e.g. B-26041).")]
    [ProducesResponseType(typeof(ProductionBatchResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBatchByNumber([FromRoute] string batchNumber, CancellationToken cancellationToken)
    {
        BatchNumber numberVo;
        try
        {
            numberVo = new BatchNumber(batchNumber);
        }
        catch (ArgumentException ex)
        {
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status400BadRequest,
                ManufacturingError.InvalidOperation,
                ex.Message);
        }

        var batch = await queryService.Handle(new GetBatchByNumberQuery(numberVo), cancellationToken);

        if (batch is null)
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status404NotFound,
                ManufacturingError.BatchNotFound,
                $"Production batch with number '{batchNumber}' was not found.");

        return Ok(ProductionBatchResourceFromEntityAssembler.ToResourceFromEntity(batch));
    }

    [HttpPatch("{id:int}/progress")]
    [EndpointSummary("Record the progress of a production batch")]
    [EndpointDescription("Records the progress, from 0 to 100, that the Web App shows for the batch.")]
    [ProducesResponseType(typeof(ProductionBatchResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBatchProgress(
        [FromRoute] int id,
        [FromBody] UpdateBatchProgressResource resource,
        CancellationToken cancellationToken)
    {
        var result = await commandService.Handle(new UpdateBatchProgressCommand(id, resource.Progress), cancellationToken);

        return ManufacturingActionResultAssembler.ToActionResult(
            this,
            result,
            problemDetailsFactory,
            batch => Ok(ProductionBatchResourceFromEntityAssembler.ToResourceFromEntity(batch)));
    }

    [HttpPost("{id:int}/close")]
    [EndpointSummary("Close a production batch")]
    [EndpointDescription("Closes an active production batch and records the manufactured quantity.")]
    [ProducesResponseType(typeof(ProductionBatchResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CloseBatch(
        [FromRoute] int id,
        [FromBody] CloseBatchResource resource,
        CancellationToken cancellationToken)
    {
        var command = CloseBatchCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await commandService.Handle(command, cancellationToken);

        return ManufacturingActionResultAssembler.ToActionResult(
            this,
            result,
            problemDetailsFactory,
            batch => Ok(ProductionBatchResourceFromEntityAssembler.ToResourceFromEntity(batch)));
    }

    [HttpPost("{id:int}/incidents")]
    [EndpointSummary("Register an incident in a production batch")]
    [EndpointDescription("Registers an operational incident occurred during batch manufacturing.")]
    [ProducesResponseType(typeof(IncidentResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegisterIncident(
        [FromRoute] int id,
        [FromBody] RegisterIncidentResource resource,
        CancellationToken cancellationToken)
    {
        var command = RegisterIncidentCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await commandService.Handle(command, cancellationToken);

        return ManufacturingActionResultAssembler.ToActionResult(
            this,
            result,
            problemDetailsFactory,
            incident => CreatedAtAction(
                nameof(GetBatchById),
                new { id },
                ProductionBatchResourceFromEntityAssembler.ToIncidentResource(incident)));
    }
}
