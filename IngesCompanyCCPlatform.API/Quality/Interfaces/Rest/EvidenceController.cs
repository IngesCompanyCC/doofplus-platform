using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Quality.Resources;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

/// <summary>
///     REST controller of the evidence of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Evidence")]
public class EvidenceController(
    IEvidenceCommandService evidenceCommandService,
    IEvidenceQueryService evidenceQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a evidence")]
    [EndpointDescription("Creates a evidence.")]
    [ProducesResponseType(typeof(EvidenceResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateEvidence([FromBody] CreateEvidenceResource resource, CancellationToken cancellationToken)
    {
        var command = EvidenceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await evidenceCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetEvidenceById), new { evidenceId = created.Id.Value },
                EvidenceResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }
    [HttpGet]
    [EndpointSummary("Get the evidence")]
    [EndpointDescription("Gets all the evidence.")]
    [ProducesResponseType(typeof(IEnumerable<EvidenceResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEvidence(CancellationToken cancellationToken)
    {
        var items = await evidenceQueryService.Handle(new GetAllEvidenceQuery(), cancellationToken);
        return Ok(items.Select(EvidenceResourceFromEntityAssembler.ToResourceFromEntity));
    }
    [HttpGet("{evidenceId:int}")]
    [EndpointSummary("Get a evidence by its id")]
    [EndpointDescription("Gets a evidence by the provided id.")]
    [ProducesResponseType(typeof(EvidenceResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEvidenceById([FromRoute] int evidenceId, CancellationToken cancellationToken)
    {
        var item = await evidenceQueryService.Handle(new GetEvidenceByIdQuery(evidenceId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, EvidenceErrors.NotFoundById(evidenceId),
            QualityError.EvidenceNotFound, messages, problemDetailsFactory,
            found => Ok(EvidenceResourceFromEntityAssembler.ToResourceFromEntity(found)), evidenceId);
    }
    [HttpGet("by-deviation/{deviationId:int}")]
    [EndpointSummary("Get the evidence of a deviation")]
    [EndpointDescription("Gets the evidence of the deviation with the provided id.")]
    [ProducesResponseType(typeof(IEnumerable<EvidenceResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEvidenceByDeviationId([FromRoute] int deviationId, CancellationToken cancellationToken)
    {
        var items = await evidenceQueryService.Handle(new GetAllEvidenceByDeviationIdQuery(deviationId), cancellationToken);
        return Ok(items.Select(EvidenceResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
