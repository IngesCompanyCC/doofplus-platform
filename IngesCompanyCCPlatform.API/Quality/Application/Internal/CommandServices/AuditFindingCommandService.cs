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
///     Command service of <see cref="AuditFinding" />.
/// </summary>
public class AuditFindingCommandService(
    IAuditFindingRepository auditFindingRepository,
    IAuditRepository auditRepository,
    IUnitOfWork unitOfWork) : IAuditFindingCommandService
{
    /// <inheritdoc />
    public async Task<Result<AuditFinding>> Handle(CreateAuditFindingCommand command, CancellationToken cancellationToken)
    {
        if (await auditRepository.FindByIdAsNoTrackingAsync(command.AuditId, cancellationToken) is null)
            return Result<AuditFinding>.Failure(AuditErrors.NotFoundById(command.AuditId), QualityError.AuditNotFound);

        var auditFinding = new AuditFinding(command);
        await auditFindingRepository.AddAsync(auditFinding, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<AuditFinding>.Success(auditFinding);
    }
}
