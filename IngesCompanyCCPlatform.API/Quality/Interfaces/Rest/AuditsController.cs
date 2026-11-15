using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
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
///     REST controller of the audits of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Audits")]
public class AuditsController(
    IAuditCommandService auditCommandService,
    IAuditQueryService auditQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a audit")]
    [EndpointDescription("Creates a audit.")]
    [ProducesResponseType(typeof(AuditResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAudit([FromBody] CreateAuditResource resource, CancellationToken cancellationToken)
    {
        var command = AuditCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await auditCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetAuditById), new { auditId = created.Id.Value },
                AuditResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the audits")]
    [EndpointDescription("Gets the audits.")]
    [ProducesResponseType(typeof(IEnumerable<AuditResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAudits(CancellationToken cancellationToken)
    {
        var items = await auditQueryService.Handle(new GetAllAuditsQuery(), cancellationToken);
        return Ok(items.Select(AuditResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{auditId:int}")]
    [EndpointSummary("Get a audit by its id")]
    [EndpointDescription("Gets a audit by the provided id.")]
    [ProducesResponseType(typeof(AuditResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuditById([FromRoute] int auditId, CancellationToken cancellationToken)
    {
        var item = await auditQueryService.Handle(new GetAuditByIdQuery(auditId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, AuditErrors.NotFoundById(auditId),
            QualityError.AuditNotFound, messages, problemDetailsFactory,
            found => Ok(AuditResourceFromEntityAssembler.ToResourceFromEntity(found)), auditId);
    }
}
