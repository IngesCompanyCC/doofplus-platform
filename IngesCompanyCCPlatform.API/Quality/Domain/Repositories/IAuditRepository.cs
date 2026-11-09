using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Represents the audit repository of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IAuditRepository : IBaseRepository<Audit>;
