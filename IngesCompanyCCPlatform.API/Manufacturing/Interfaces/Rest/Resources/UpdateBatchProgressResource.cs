namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Request payload to record the progress of a production batch.
/// </summary>
/// <param name="Progress">The progress, from 0 to 100.</param>
public record UpdateBatchProgressResource(int Progress);
