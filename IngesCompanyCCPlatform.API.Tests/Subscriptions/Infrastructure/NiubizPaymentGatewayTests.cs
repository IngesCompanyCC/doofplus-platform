using System.Net;
using System.Text;
using IngesCompanyCCPlatform.API.Subscriptions.Application.OutboundServices.PaymentGateway;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Subscriptions.Infrastructure.PaymentGateway.Niubiz;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace IngesCompanyCCPlatform.API.Tests.Subscriptions.Infrastructure;

/// <summary>
///     Tests of <see cref="NiubizPaymentGateway" /> against a fake Niubiz: it never calls the real sandbox.
/// </summary>
public class NiubizPaymentGatewayTests
{
    private static readonly Money Amount = new(199m, "USD");

    private static readonly NiubizSettings Configured = new()
    {
        BaseUrl = "https://niubiz.test", MerchantId = "456879852", Username = "sandbox-user", Password = "sandbox-pass"
    };

    private static NiubizPaymentGateway Gateway(FakeNiubiz niubiz, NiubizSettings settings) =>
        new(new HttpClient(niubiz), Options.Create(settings), NullLogger<NiubizPaymentGateway>.Instance);

    [Fact]
    public async Task AuthorizeShouldReturnTheTransactionWhenNiubizApproves()
    {
        var niubiz = new FakeNiubiz(HttpStatusCode.OK,
            """{"dataMap":{"ACTION_CODE":"000","TRANSACTION_ID":"993201","ACTION_DESCRIPTION":"Aprobado y completado con exito"}}""");

        var result = await Gateway(niubiz, Configured).AuthorizeAsync(Amount, "7B4E2A1C9D8F4A6B");

        Assert.Equal(EPaymentOutcome.Approved, result.Outcome);
        Assert.Equal("993201", result.TransactionId);
        Assert.Equal("/api.security/v1/security", niubiz.Paths[0]);
        Assert.Equal("/api.authorization/v3/authorization/ecommerce/456879852", niubiz.Paths[1]);
        Assert.Contains("\"tokenId\":\"7B4E2A1C9D8F4A6B\"", niubiz.AuthorizationBody);
    }

    [Fact]
    public async Task AuthorizeShouldReturnTheReasonWhenNiubizDenies()
    {
        var niubiz = new FakeNiubiz(HttpStatusCode.BadRequest,
            """{"data":{"ACTION_CODE":"180","ACTION_DESCRIPTION":"Tarjeta no valida"}}""");

        var result = await Gateway(niubiz, Configured).AuthorizeAsync(Amount, "7B4E2A1C9D8F4A6B");

        Assert.Equal(EPaymentOutcome.Rejected, result.Outcome);
        Assert.Equal("Tarjeta no valida", result.Message);
    }

    [Fact]
    public async Task AuthorizeShouldRejectWhenTheActionCodeIsNotApproved()
    {
        var niubiz = new FakeNiubiz(HttpStatusCode.OK,
            """{"dataMap":{"ACTION_CODE":"116","ACTION_DESCRIPTION":"Fondos insuficientes"}}""");

        var result = await Gateway(niubiz, Configured).AuthorizeAsync(Amount, "7B4E2A1C9D8F4A6B");

        Assert.Equal(EPaymentOutcome.Rejected, result.Outcome);
    }

    [Fact]
    public async Task AuthorizeShouldBeUnavailableWhenNiubizFails()
    {
        var niubiz = new FakeNiubiz(HttpStatusCode.ServiceUnavailable, "{}");

        var result = await Gateway(niubiz, Configured).AuthorizeAsync(Amount, "7B4E2A1C9D8F4A6B");

        Assert.Equal(EPaymentOutcome.Unavailable, result.Outcome);
    }

    [Fact]
    public async Task AuthorizeShouldBeUnavailableWhenNiubizCannotBeReached()
    {
        var niubiz = new FakeNiubiz(HttpStatusCode.OK, "{}") { Unreachable = true };

        var result = await Gateway(niubiz, Configured).AuthorizeAsync(Amount, "7B4E2A1C9D8F4A6B");

        Assert.Equal(EPaymentOutcome.Unavailable, result.Outcome);
    }

    [Fact]
    public async Task AuthorizeShouldNotCallNiubizWhenItIsNotConfigured()
    {
        var niubiz = new FakeNiubiz(HttpStatusCode.OK, "{}");

        var result = await Gateway(niubiz, new NiubizSettings()).AuthorizeAsync(Amount, "7B4E2A1C9D8F4A6B");

        Assert.Equal(EPaymentOutcome.Unavailable, result.Outcome);
        Assert.Empty(niubiz.Paths);
    }

    /// <summary>
    ///     Answers the security request with a token and the authorization request with the given response.
    /// </summary>
    private sealed class FakeNiubiz(HttpStatusCode authorizationStatus, string authorizationBody) : HttpMessageHandler
    {
        public List<string> Paths { get; } = [];
        public string AuthorizationBody { get; private set; } = string.Empty;
        public bool Unreachable { get; init; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (Unreachable) throw new HttpRequestException("Connection refused");
            Paths.Add(request.RequestUri!.AbsolutePath);
            if (request.Method == HttpMethod.Get)
                return new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent("security-token") };
            AuthorizationBody = await request.Content!.ReadAsStringAsync(cancellationToken);
            return new HttpResponseMessage(authorizationStatus)
            {
                Content = new StringContent(authorizationBody, Encoding.UTF8, "application/json")
            };
        }
    }
}
