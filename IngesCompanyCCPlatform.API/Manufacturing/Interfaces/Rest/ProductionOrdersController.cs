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
///     REST controller of the production orders and their operations.
/// </summary>
[ApiController]
[Route("api/v1/production-orders")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Production orders")]
public class ProductionOrdersController(
    IProductionOrderCommandService productionOrderCommandService,
    IProductionOrderQueryService productionOrderQueryService,
    IStringLocalizer<ManufacturingMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Plan a production order")]
    [EndpointDescription("Plans an order with its operations; the formula must be approved.")]
    [ProducesResponseType(typeof(ProductionOrderResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateProductionOrder([FromBody] CreateProductionOrderResource resource, CancellationToken cancellationToken)
    {
        var result = await productionOrderCommandService.Handle(CatalogCommandFromResourceAssembler.ToCommandFromResource(resource), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetProductionOrderById), new { productionOrderId = created.Id.Value }, CatalogResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get all production orders")]
    [EndpointDescription("Gets all the orders with their operations.")]
    [ProducesResponseType(typeof(IEnumerable<ProductionOrderResource>), StatusCodes.Status200OK)]

    public async Task<IActionResult> GetAllProductionOrders(CancellationToken cancellationToken)
    {
        var orders = await productionOrderQueryService.Handle(new GetAllProductionOrdersQuery(), cancellationToken);
        return Ok(orders.Select(CatalogResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{productionOrderId:int}")]
    [EndpointSummary("Get a production order by its id")]
    [EndpointDescription("Gets an order with its operations by the provided id.")]
    [ProducesResponseType(typeof(ProductionOrderResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductionOrderById([FromRoute] int productionOrderId, CancellationToken cancellationToken)
    {
        var entity = await productionOrderQueryService.Handle(new GetProductionOrderByIdQuery(productionOrderId), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromQueryResult(this, entity, ProductionOrderErrors.NotFoundById(productionOrderId),
            ManufacturingError.ProductionOrderNotFound, messages, problemDetailsFactory, found => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(found)), productionOrderId);
    }

    [HttpPut("{productionOrderId:int}/status")]
    [EndpointSummary("Change the status of a production order")]
    [EndpointDescription("Starts (InProgress), holds (OnHold), resumes (InProgress) or completes (Completed) an order; 409 when the change is not allowed.")]
    [ProducesResponseType(typeof(ProductionOrderResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ChangeProductionOrderStatus([FromRoute] int productionOrderId, [FromBody] ChangeProductionOrderStatusResource resource, CancellationToken cancellationToken)
    {
        var result = await productionOrderCommandService.Handle(CatalogCommandFromResourceAssembler.ToCommandFromResource(productionOrderId, resource), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            changed => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(changed)));
    }

    [HttpPut("{productionOrderId:int}/operations/{sequence:int}")]
    [EndpointSummary("Record the progress of an operation")]
    [EndpointDescription("Records the progress (0 to 100), status and reference of an operation of the order.")]
    [ProducesResponseType(typeof(ProductionOrderResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOperation([FromRoute] int productionOrderId, [FromRoute] int sequence, [FromBody] UpdateOperationResource resource, CancellationToken cancellationToken)
    {
        var result = await productionOrderCommandService.Handle(CatalogCommandFromResourceAssembler.ToCommandFromResource(productionOrderId, sequence, resource), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            updated => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }
}
