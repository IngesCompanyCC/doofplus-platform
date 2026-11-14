namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;

/// <summary>
///     Query to retrieve a production batch by its unique database identifier.
/// </summary>
public record GetBatchByIdQuery(int Id);
