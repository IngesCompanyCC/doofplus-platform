namespace IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Marks an action that does not need a signed-in user.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{
}
