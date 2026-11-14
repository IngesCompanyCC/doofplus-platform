namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Request to release a production batch.
/// </summary>
/// <param name="ReleasedBy">Who signs the release, for example the QA manager.</param>
public record ReleaseBatchResource(string ReleasedBy);
