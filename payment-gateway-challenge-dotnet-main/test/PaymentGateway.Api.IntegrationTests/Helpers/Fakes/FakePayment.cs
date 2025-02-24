using PaymentGateway.Api.Entities;
using PaymentGateway.Api.Enums;

namespace PaymentGateway.Api.IntegrationTests.Helpers.Fakes;

/// <summary>
/// I like adding fakes like this rather than creating objects in the test methods, this way it's clearer which
/// properties are important to the test and which have arbitrary values.
/// </summary>
public static class FakePayment
{
    public static Payment CreateValid(Random random)
    {
        return new Payment
        {
            Id = Guid.NewGuid(),
            ExpiryYear = random.Next(2023,2030),
            ExpiryMonth = random.Next(1, 12),
            Amount = random.Next(1, 10000),
            CardNumberLastFour = random.Next(1111, 9999).ToString(),
            Currency = CurrencyCode.GBP,
            Status = PaymentStatus.Authorized
        };
    }
}