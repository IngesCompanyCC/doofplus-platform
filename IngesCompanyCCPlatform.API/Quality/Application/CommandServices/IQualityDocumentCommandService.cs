using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="QualityDocument" />.
/// </summary>
public interface IQualityDocumentCommandService
{
    /// <summary>
    ///     Creates a quality document.
    /// </summary>
    Task<Result<QualityDocument>> Handle(CreateQualityDocumentCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Replaces the data of a quality document.
    /// </summary>
    Task<Result<QualityDocument>> Handle(UpdateQualityDocumentCommand command, CancellationToken cancellationToken);
}
