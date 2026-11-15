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
///     Command service of <see cref="AuditEvent" />.
/// </summary>
public class AuditEventCommandService(
    IAuditEventRepository auditEventRepository,
    IUnitOfWork unitOfWork) : IAuditEventCommandService
{
    /// <inheritdoc />
    public async Task<Result<AuditEvent>> Handle(CreateAuditEventCommand command, CancellationToken cancellationToken)
    {
        var auditEvent = new AuditEvent(command);
        await auditEventRepository.AddAsync(auditEvent, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<AuditEvent>.Success(auditEvent);
    }
}
