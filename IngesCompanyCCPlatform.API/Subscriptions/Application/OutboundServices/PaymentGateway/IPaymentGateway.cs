using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Application.OutboundServices.PaymentGateway;

/// <summary>
///     Payment gateway that charges a card tokenized on the client (Niubiz).
/// </summary>
public interface IPaymentGateway
{
    /// <summary>
    ///     Authorizes a charge.
    /// </summary>
    /// <param name="amount">The amount to charge.</param>
    /// <param name="cardToken">The card token created by the payment form.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Approved, rejected or unavailable.</returns>
    Task<PaymentResult> AuthorizeAsync(Money amount, string cardToken, CancellationToken cancellationToken = default);
}
