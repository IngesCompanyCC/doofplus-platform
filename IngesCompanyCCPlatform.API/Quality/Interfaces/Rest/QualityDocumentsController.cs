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
///     REST controller of the quality documents of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Quality Documents")]
public class QualityDocumentsController(
    IQualityDocumentCommandService qualityDocumentCommandService,
    IQualityDocumentQueryService qualityDocumentQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a quality document")]
    [EndpointDescription("Creates a quality document.")]
    [ProducesResponseType(typeof(QualityDocumentResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateQualityDocument([FromBody] CreateQualityDocumentResource resource, CancellationToken cancellationToken)
    {
        var command = QualityDocumentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await qualityDocumentCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetQualityDocumentById), new { qualityDocumentId = created.Id.Value },
                QualityDocumentResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the quality documents")]
    [EndpointDescription("Gets the quality documents; with code, only the ones with that code.")]
    [ProducesResponseType(typeof(IEnumerable<QualityDocumentResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetQualityDocuments([FromQuery] string? code, CancellationToken cancellationToken)
    {
        var items = await qualityDocumentQueryService.Handle(new GetAllQualityDocumentsQuery(code), cancellationToken);
        return Ok(items.Select(QualityDocumentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{qualityDocumentId:int}")]
    [EndpointSummary("Get a quality document by its id")]
    [EndpointDescription("Gets a quality document by the provided id.")]
    [ProducesResponseType(typeof(QualityDocumentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetQualityDocumentById([FromRoute] int qualityDocumentId, CancellationToken cancellationToken)
    {
        var item = await qualityDocumentQueryService.Handle(new GetQualityDocumentByIdQuery(qualityDocumentId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, QualityDocumentErrors.NotFoundById(qualityDocumentId),
            QualityError.QualityDocumentNotFound, messages, problemDetailsFactory,
            found => Ok(QualityDocumentResourceFromEntityAssembler.ToResourceFromEntity(found)), qualityDocumentId);
    }

    [HttpPut("{qualityDocumentId:int}")]
    [EndpointSummary("Update a quality document")]
    [EndpointDescription("Replaces the data of the quality document with the provided id.")]
    [ProducesResponseType(typeof(QualityDocumentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateQualityDocument([FromRoute] int qualityDocumentId, [FromBody] UpdateQualityDocumentResource resource, CancellationToken cancellationToken)
    {
        var command = QualityDocumentCommandFromResourceAssembler.ToCommandFromResource(qualityDocumentId, resource);
        var result = await qualityDocumentCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            updated => Ok(QualityDocumentResourceFromEntityAssembler.ToResourceFromEntity(updated)), qualityDocumentId);
    }
}
