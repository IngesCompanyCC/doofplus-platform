using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Filters;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Audits")]
[QualityInvalidDataFilter]
public class AuditsController(
    IAuditCommandService auditCommandService,
    IAuditQueryService auditQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all audits")]
    [ProducesResponseType(typeof(IEnumerable<AuditResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAudits(CancellationToken cancellationToken)
    {
        var audits = await auditQueryService.Handle(new GetAllAuditsQuery(), cancellationToken);
        return Ok(audits.Select(AuditResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{auditId:int}")]
    [EndpointSummary("Get an audit by id")]
    [ProducesResponseType(typeof(AuditResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuditById(int auditId, CancellationToken cancellationToken)
    {
        var audit = await auditQueryService.Handle(new GetAuditByIdQuery(auditId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQuery(this, audit, problemDetailsFactory,
            QualityError.AuditNotFound, QualityErrors.AuditNotFound(auditId).Message,
            found => Ok(AuditResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [EndpointSummary("Schedule an audit")]
    [ProducesResponseType(typeof(AuditResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ScheduleAudit([FromBody] ScheduleAuditResource resource,
        CancellationToken cancellationToken)
    {
        var command = AuditCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await auditCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetAuditById), new { auditId = created.Id.Value },
                AuditResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPost("{auditId:int}/start")]
    [EndpointSummary("Start a scheduled audit")]
    [ProducesResponseType(typeof(AuditResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> StartAudit(int auditId, CancellationToken cancellationToken)
    {
        return ToOk(await auditCommandService.Handle(new StartAuditCommand(auditId), cancellationToken));
    }

    [HttpPost("{auditId:int}/findings")]
    [EndpointSummary("Register a finding or an observation")]
    [ProducesResponseType(typeof(AuditResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterAuditFinding(int auditId,
        [FromBody] RegisterAuditFindingResource resource, CancellationToken cancellationToken)
    {
        var command = AuditCommandFromResourceAssembler.ToCommandFromResource(auditId, resource);
        return ToOk(await auditCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{auditId:int}/evidence-package")]
    [EndpointSummary("Attach the evidence package of an audit")]
    [ProducesResponseType(typeof(AuditResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AttachAuditEvidencePackage(int auditId,
        [FromBody] AttachAuditEvidencePackageResource resource, CancellationToken cancellationToken)
    {
        var command = AuditCommandFromResourceAssembler.ToCommandFromResource(auditId, resource);
        return ToOk(await auditCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{auditId:int}/closure")]
    [EndpointSummary("Close an audit")]
    [ProducesResponseType(typeof(AuditResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CloseAudit(int auditId, CancellationToken cancellationToken)
    {
        return ToOk(await auditCommandService.Handle(new CloseAuditCommand(auditId), cancellationToken));
    }

    private IActionResult ToOk(Result<Audit> result)
    {
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            audit => Ok(AuditResourceFromEntityAssembler.ToResourceFromEntity(audit)));
    }
}
