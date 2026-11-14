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
///     REST controller of the products of the laboratory (TS07).
/// </summary>
[ApiController]
[Route("api/v1/products")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Products")]
public class ProductsController(
    IProductCommandService productCommandService,
    IProductQueryService productQueryService,
    IStringLocalizer<ManufacturingMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Register a product")]
    [EndpointDescription("Registers a product with its pharmaceutical form.")]
    [ProducesResponseType(typeof(ProductResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource resource, CancellationToken cancellationToken)
    {
        var result = await productCommandService.Handle(CatalogCommandFromResourceAssembler.ToCommandFromResource(resource), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetProductById), new { productId = created.Id.Value }, CatalogResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get all products")]
    [EndpointDescription("Gets all the products.")]
    [ProducesResponseType(typeof(IEnumerable<ProductResource>), StatusCodes.Status200OK)]

    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        var products = await productQueryService.Handle(new GetAllProductsQuery(), cancellationToken);
        return Ok(products.Select(CatalogResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{productId:int}")]
    [EndpointSummary("Get a product by its id")]
    [EndpointDescription("Gets a product by the provided id.")]
    [ProducesResponseType(typeof(ProductResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById([FromRoute] int productId, CancellationToken cancellationToken)
    {
        var entity = await productQueryService.Handle(new GetProductByIdQuery(productId), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromQueryResult(this, entity, ProductErrors.NotFoundById(productId),
            ManufacturingError.ProductNotFound, messages, problemDetailsFactory, found => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(found)), productId);
    }

    [HttpPut("{productId:int}")]
    [EndpointSummary("Update a product")]
    [EndpointDescription("Updates the name and form of a product; its code does not change.")]
    [ProducesResponseType(typeof(ProductResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromBody] UpdateProductResource resource, CancellationToken cancellationToken)
    {
        var result = await productCommandService.Handle(CatalogCommandFromResourceAssembler.ToCommandFromResource(productId, resource), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            updated => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }
}
