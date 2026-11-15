using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Tests.Subscriptions.Domain;

/// <summary>
///     Unit tests of the Payment entity, the Plan aggregate and the Subscriptions value objects.
/// </summary>
public class PaymentAndPlanTests
{
    [Fact]
    public void NewPaymentShouldBePendingWithTheAmountAndCurrency()
    {
        var payment = new Payment("INV-1-1", new Money(199m, "USD"));

        Assert.Equal(EPaymentStatus.Pending, payment.Status);
        Assert.Equal(199m, payment.Amount);
        Assert.Equal("USD", payment.Currency);
        Assert.Null(payment.ProcessedAt);
    }

    [Fact]
    public void ApproveShouldKeepTheTransactionId()
    {
        var payment = new Payment("INV-1-1", new Money(199m, "USD"));

        payment.Approve("993201");

        Assert.True(payment.IsApproved);
        Assert.Equal("993201", payment.GatewayTransactionId);
        Assert.NotNull(payment.ProcessedAt);
    }

    [Fact]
    public void RejectShouldMarkThePaymentAsRejected()
    {
        var payment = new Payment("INV-1-1", new Money(199m, "USD"));

        payment.Reject();

        Assert.Equal(EPaymentStatus.Rejected, payment.Status);
        Assert.NotNull(payment.ProcessedAt);
    }

    [Fact]
    public void APaymentShouldBeProcessedOnlyOnce()
    {
        var payment = new Payment("INV-1-1", new Money(199m, "USD"));
        payment.Reject();

        var exception = Assert.Throws<InvalidOperationException>(() => payment.Approve("993201"));

        Assert.Equal("payment.error.notPending", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void APaymentShouldNeedAnInvoiceNumber(string invoiceNumber)
    {
        Assert.Throws<ArgumentException>(() => new Payment(invoiceNumber, new Money(199m, "USD")));
    }

    [Fact]
    public void PlanShouldReturnThePriceOfEachBillingCycle()
    {
        var plan = SubscriptionTestData.StandardLabPlan();

        Assert.Equal(new Money(199m, "USD"), plan.GetPrice(EBillingCycle.Monthly));
        Assert.Equal(new Money(1990m, "USD"), plan.GetPrice(EBillingCycle.Annual));
    }

    [Fact]
    public void PlanShouldRejectPricesInDifferentCurrencies()
    {
        var command = new CreatePlanCommand(new PlanKey("standard-lab"), "Standard Lab",
            new Money(199m, "USD"), new Money(1990m, "PEN"), 10, 5);

        var exception = Assert.Throws<ArgumentException>(() => new Plan(command));

        Assert.Equal("plan.error.currency.mismatch", exception.Message);
    }

    [Fact]
    public void PlanShouldRejectNegativeLimits()
    {
        var command = new CreatePlanCommand(new PlanKey("standard-lab"), "Standard Lab",
            new Money(199m, "USD"), new Money(1990m, "USD"), -1, 5);

        Assert.Throws<ArgumentException>(() => new Plan(command));
    }

    [Theory]
    [InlineData("Standard Lab")]
    [InlineData("standard_lab")]
    [InlineData("")]
    public void PlanKeyShouldBeLowercaseWordsJoinedByHyphens(string value)
    {
        Assert.Throws<ArgumentException>(() => new PlanKey(value));
    }

    [Theory]
    [InlineData(-1, "USD")]
    [InlineData(10, "usd")]
    [InlineData(10, "US")]
    public void MoneyShouldRejectANegativeAmountOrAnInvalidCurrency(decimal amount, string currency)
    {
        Assert.Throws<ArgumentException>(() => new Money(amount, currency));
    }

    [Theory]
    [InlineData("Visa", "424", "08/2028")]
    [InlineData("Visa", "4242", "13/2028")]
    [InlineData("", "4242", "08/2028")]
    public void PaymentCardShouldRejectInvalidData(string brand, string last4, string expiry)
    {
        Assert.Throws<ArgumentException>(() => new PaymentCard(brand, last4, expiry));
    }

    [Theory]
    [InlineData("billing")]
    [InlineData("billing@")]
    [InlineData("")]
    public void BillingEmailShouldRejectAnInvalidEmail(string value)
    {
        Assert.Throws<ArgumentException>(() => new BillingEmail(value));
    }

    [Fact]
    public void OrganizationIdShouldBePositive()
    {
        Assert.Throws<ArgumentException>(() => new OrganizationId(0));
    }
}
