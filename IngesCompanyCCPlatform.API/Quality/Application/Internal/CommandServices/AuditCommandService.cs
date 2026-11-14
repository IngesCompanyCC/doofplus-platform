using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Command service of <see cref="Audit" />.
/// </summary>
public class AuditCommandService(
    IAuditRepository auditRepository,
    IUnitOfWork unitOfWork) : IAuditCommandService
{
    /// <inheritdoc />
    public async Task<Result<Audit>> Handle(CreateAuditCommand command, CancellationToken cancellationToken)
    {
        if (await auditRepository.ExistsByCodeAsync(command.Code.Trim(), cancellationToken))
            return Result<Audit>.Failure(AuditErrors.DuplicateCode, QualityError.DuplicateAuditCode);

        var audit = new Audit(command);
        await auditRepository.AddAsync(audit, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Audit>.Success(audit);
    }
}
