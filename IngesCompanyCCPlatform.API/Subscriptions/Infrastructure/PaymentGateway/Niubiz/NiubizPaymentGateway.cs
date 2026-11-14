using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using IngesCompanyCCPlatform.API.Subscriptions.Application.OutboundServices.PaymentGateway;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;
using Microsoft.Extensions.Options;

namespace IngesCompanyCCPlatform.API.Subscriptions.Infrastructure.PaymentGateway.Niubiz;

/// <summary>
///     Niubiz implementation of <see cref="IPaymentGateway" />: gets a security token and authorizes the charge.
/// </summary>
public class NiubizPaymentGateway(HttpClient httpClient, IOptions<NiubizSettings> options, ILogger<NiubizPaymentGateway> logger)
    : IPaymentGateway
{
    private const string ApprovedActionCode = "000";
    private const string DefaultRejectionMessage = "Charge rejected by the payment gateway";
    private const string SecurityPath = "api.security/v1/security";
    private const string AuthorizationPath = "api.authorization/v3/authorization/ecommerce/";

    /// <inheritdoc />
    public async Task<PaymentResult> AuthorizeAsync(Money amount, string cardToken, CancellationToken cancellationToken = default)
    {
        var settings = options.Value;
        if (string.IsNullOrWhiteSpace(settings.MerchantId) || string.IsNullOrWhiteSpace(settings.Username) ||
            string.IsNullOrWhiteSpace(settings.Password))
        {
            logger.LogWarning("Niubiz is not configured: set Niubiz__MerchantId, Niubiz__Username and Niubiz__Password");
            return PaymentResult.UnavailableWith("Payment gateway is not configured");
        }

        try
        {
            var baseUrl = settings.BaseUrl.TrimEnd('/') + "/";
            using var securityRequest = new HttpRequestMessage(HttpMethod.Get, baseUrl + SecurityPath);
            securityRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{settings.Username}:{settings.Password}")));
            using var securityResponse = await httpClient.SendAsync(securityRequest, cancellationToken);
            if ((int)securityResponse.StatusCode >= 500) return Unavailable(securityResponse);
            securityResponse.EnsureSuccessStatusCode();
            var securityToken = await securityResponse.Content.ReadAsStringAsync(cancellationToken);

            using var authorizationRequest = new HttpRequestMessage(HttpMethod.Post, baseUrl + AuthorizationPath + settings.MerchantId)
            {
                Content = JsonContent.Create(new
                {
                    channel = "web",
                    captureType = "manual",
                    countable = true,
                    order = new
                    {
                        tokenId = cardToken,
                        purchaseNumber = Random.Shared.NextInt64(100_000_000_000L, 1_000_000_000_000L),
                        amount = amount.Amount,
                        currency = amount.Currency
                    }
                })
            };
            authorizationRequest.Headers.TryAddWithoutValidation("Authorization", securityToken);
            using var response = await httpClient.SendAsync(authorizationRequest, cancellationToken);
            if ((int)response.StatusCode >= 500) return Unavailable(response);

            using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync(cancellationToken));
            if (!response.IsSuccessStatusCode)
            {
                var reason = ActionDescription(body.RootElement);
                logger.LogWarning("Niubiz rejected the charge: status={Status}, reason={Reason}", (int)response.StatusCode, reason);
                return PaymentResult.RejectedWith(reason);
            }

            var data = Field(body.RootElement, "dataMap");
            if (Text(data, "ACTION_CODE") != ApprovedActionCode)
                return PaymentResult.RejectedWith(ActionDescription(body.RootElement));
            return PaymentResult.ApprovedWith(Text(data, "TRANSACTION_ID"), Text(data, "ACTION_DESCRIPTION"));
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException or JsonException)
        {
            logger.LogError("Niubiz is not available: {Message}", exception.Message);
            return PaymentResult.UnavailableWith("Payment gateway is not available");
        }
    }

    private PaymentResult Unavailable(HttpResponseMessage response)
    {
        logger.LogError("Niubiz failed: status={Status}", (int)response.StatusCode);
        return PaymentResult.UnavailableWith("Payment gateway is not available");
    }

    private static string ActionDescription(JsonElement body)
    {
        var description = Text(Field(body, "data"), "ACTION_DESCRIPTION");
        if (string.IsNullOrWhiteSpace(description)) description = Text(Field(body, "dataMap"), "ACTION_DESCRIPTION");
        return string.IsNullOrWhiteSpace(description) ? DefaultRejectionMessage : description;
    }

    private static JsonElement? Field(JsonElement? body, string name)
    {
        return body is { ValueKind: JsonValueKind.Object } element && element.TryGetProperty(name, out var value) &&
               value.ValueKind == JsonValueKind.Object
            ? value
            : null;
    }

    private static string Text(JsonElement? body, string name)
    {
        return body is { ValueKind: JsonValueKind.Object } element && element.TryGetProperty(name, out var value)
            ? value.ToString()
            : string.Empty;
    }
}
