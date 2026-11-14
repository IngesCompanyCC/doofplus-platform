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
///     REST controller of the master formulas of the products (TS07).
/// </summary>
[ApiController]
[Route("api/v1/master-formulas")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Master formulas")]
public class MasterFormulasController(
    IMasterFormulaCommandService masterFormulaCommandService,
    IMasterFormulaQueryService masterFormulaQueryService,
    IStringLocalizer<ManufacturingMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Write a master formula")]
    [EndpointDescription("Writes a new version of the formula of an existing product with its components and parameters; it starts as Draft.")]
    [ProducesResponseType(typeof(MasterFormulaResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateMasterFormula([FromBody] CreateMasterFormulaResource resource, CancellationToken cancellationToken)
    {
        var result = await masterFormulaCommandService.Handle(CatalogCommandFromResourceAssembler.ToCommandFromResource(resource), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetMasterFormulaById), new { masterFormulaId = created.Id.Value }, CatalogResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the master formulas")]
    [EndpointDescription("Gets all the formulas, or the ones of the product with the given code.")]
    [ProducesResponseType(typeof(IEnumerable<MasterFormulaResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMasterFormulas([FromQuery] string? productCode, CancellationToken cancellationToken)
    {
        var formulas = string.IsNullOrWhiteSpace(productCode)
            ? await masterFormulaQueryService.Handle(new GetAllMasterFormulasQuery(), cancellationToken)
            : await masterFormulaQueryService.Handle(
                new GetAllMasterFormulasByProductCodeQuery(new ProductCode(productCode)), cancellationToken);
        return Ok(formulas.Select(CatalogResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{masterFormulaId:int}")]
    [EndpointSummary("Get a master formula by its id")]
    [EndpointDescription("Gets a formula with its lines by the provided id.")]
    [ProducesResponseType(typeof(MasterFormulaResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMasterFormulaById([FromRoute] int masterFormulaId, CancellationToken cancellationToken)
    {
        var entity = await masterFormulaQueryService.Handle(new GetMasterFormulaByIdQuery(masterFormulaId), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromQueryResult(this, entity, MasterFormulaErrors.NotFoundById(masterFormulaId),
            ManufacturingError.MasterFormulaNotFound, messages, problemDetailsFactory, found => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(found)), masterFormulaId);
    }

    [HttpPost("{masterFormulaId:int}/approval")]
    [EndpointSummary("Approve a master formula")]
    [EndpointDescription("Approves a formula in Draft; the version approved before becomes Locked.")]
    [ProducesResponseType(typeof(MasterFormulaResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ApproveMasterFormula([FromRoute] int masterFormulaId, [FromBody] ApproveMasterFormulaResource resource, CancellationToken cancellationToken)
    {
        var result = await masterFormulaCommandService.Handle(new ApproveMasterFormulaCommand(masterFormulaId, resource.ApprovedBy), cancellationToken);
        return ManufacturingActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            approved => Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(approved)));
    }
}
