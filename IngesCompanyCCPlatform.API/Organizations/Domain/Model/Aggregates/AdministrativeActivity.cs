namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;

/// <summary>
///     An entry of the recent administrative activity shown in the Administration overview of the Web App.
/// </summary>
/// <remarks>
///     It is recorded by the Organizations command service when an organization is registered or a facility is added.
/// </remarks>
public class AdministrativeActivity
{
    private const string TitleNotBlankMessage = "The title of the activity is required.";

    /// <summary>
    ///     Initializes a new instance of the <see cref="AdministrativeActivity" /> class. Used by Entity Framework Core.
    /// </summary>
    private AdministrativeActivity()
    {
    }

    /// <summary>
    ///     Initializes a new activity that happened now.
    /// </summary>
    /// <param name="organizationId">The organization where it happened.</param>
    /// <param name="title">What happened, for example <c>Facility Planta Lima added</c>.</param>
    /// <param name="actor">Who did it.</param>
    /// <exception cref="ArgumentException">Thrown when the title is blank.</exception>
    public AdministrativeActivity(int organizationId, string title, string actor)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(TitleNotBlankMessage);
        OrganizationId = organizationId;
        Title = title.Trim();
        Actor = actor?.Trim() ?? string.Empty;
        OccurredAt = DateTime.UtcNow;
    }

    /// <summary>Gets the identifier of the activity.</summary>
    public int Id { get; private set; }

    /// <summary>Gets the organization where the activity happened.</summary>
    public int OrganizationId { get; private set; }

    /// <summary>Gets what happened.</summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>Gets who did it.</summary>
    public string Actor { get; private set; } = string.Empty;

    /// <summary>Gets when it happened, in UTC.</summary>
    public DateTime OccurredAt { get; private set; }
}
