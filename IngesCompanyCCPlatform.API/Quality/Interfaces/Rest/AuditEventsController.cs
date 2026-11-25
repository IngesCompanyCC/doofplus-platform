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
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

/// <summary>
///     REST controller of the audit events of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Audit Events")]
public class AuditEventsController(
    IAuditEventCommandService auditEventCommandService,
    IAuditEventQueryService auditEventQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a audit event")]
    [EndpointDescription("Creates a audit event.")]
    [ProducesResponseType(typeof(AuditEventResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAuditEvent([FromBody] CreateAuditEventResource resource, CancellationToken cancellationToken)
    {
        var command = AuditEventCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await auditEventCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetAuditEventById), new { auditEventId = created.Id.Value },
                AuditEventResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the audit events")]
    [EndpointDescription("Gets the audit events; with record, only the ones with that record.")]
    [ProducesResponseType(typeof(IEnumerable<AuditEventResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuditEvents([FromQuery] string? record, CancellationToken cancellationToken)
    {
        var items = await auditEventQueryService.Handle(new GetAllAuditEventsQuery(record), cancellationToken);
        return Ok(items.Select(AuditEventResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{auditEventId:int}")]
    [EndpointSummary("Get a audit event by its id")]
    [EndpointDescription("Gets a audit event by the provided id.")]
    [ProducesResponseType(typeof(AuditEventResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuditEventById([FromRoute] int auditEventId, CancellationToken cancellationToken)
    {
        var item = await auditEventQueryService.Handle(new GetAuditEventByIdQuery(auditEventId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, AuditEventErrors.NotFoundById(auditEventId),
            QualityError.AuditEventNotFound, messages, problemDetailsFactory,
            found => Ok(AuditEventResourceFromEntityAssembler.ToResourceFromEntity(found)), auditEventId);
    }
}
