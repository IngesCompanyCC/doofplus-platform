using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="AuditEvent" />.
/// </summary>
public interface IAuditEventQueryService
{
    /// <summary>
    ///     Gets the audit events, filtered by record when it is sent.
    /// </summary>
    Task<IEnumerable<AuditEvent>> Handle(GetAllAuditEventsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a audit event by its id, or <c>null</c>.
    /// </summary>
    Task<AuditEvent?> Handle(GetAuditEventByIdQuery query, CancellationToken cancellationToken);
}
