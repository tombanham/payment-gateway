using PaymentGateway.Api.Enums;
using PaymentGateway.Api.Models.Requests;

namespace PaymentGateway.Api.IntegrationTests.Helpers.Fakes;

public static class FakePostPaymentRequest
{
    public static PostPaymentRequest CreateValid(Random random, TimeProvider timeProvider)
    {
        return new PostPaymentRequest
        {
            CardNumber = BankSimulatorConstants.CardNumber.AuthorizedResponse,
            ExpiryYear = random.Next(timeProvider.GetUtcNow().Year + 1, 2030),
            ExpiryMonth = random.Next(1, 12),
            Currency = CurrencyCode.GBP.ToString(),
            Amount = random.Next(1, 10000),
            Cvv = random.Next(100, 999).ToString()
        };
    }
    
    public static PostPaymentRequest CreateInvalid()
    {
        return new PostPaymentRequest
        {
            CardNumber = "invalid",
            ExpiryMonth = 0,
            ExpiryYear = 0,
            Currency = "invalid",
            Amount = -1,
            Cvv = "AAA"
        };
    }
}