namespace IngesCompanyCCPlatform.API.Subscriptions.Infrastructure.PaymentGateway.Niubiz;

/// <summary>
///     Settings of the Niubiz payment gateway, read from the <c>Niubiz</c> section of the configuration.
/// </summary>
/// <remarks>
///     The merchant and the credentials come from environment variables in production
///     (<c>Niubiz__MerchantId</c>, <c>Niubiz__Username</c> and <c>Niubiz__Password</c>).
/// </remarks>
public class NiubizSettings
{
    public string BaseUrl { get; set; } = "https://apisandbox.vnforappstest.com";
    public string MerchantId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
