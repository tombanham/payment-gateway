using PaymentGateway.Api.Models.Requests;

namespace PaymentGateway.Api.Mappers;

public static class AcquiringBankPostPaymentRequestMapper
{
    public static AcquiringBankPostPaymentRequest ToAcquiringBankRequest(
        this PostPaymentRequest postPaymentRequest)
    {
        return new AcquiringBankPostPaymentRequest
        {
            CardNumber = postPaymentRequest.CardNumber,
            ExpiryDate = GetExpiryDate(postPaymentRequest.ExpiryYear, postPaymentRequest.ExpiryMonth),
            Currency = postPaymentRequest.Currency,
            Amount = postPaymentRequest.Amount,
            Cvv = postPaymentRequest.Cvv
        };
    }

    /// <summary>
    /// Formatting the expiry date property for the acquiring bank request to look like a card expiry.
    /// </summary>
    private static string GetExpiryDate(int expiryYear, int expiryMonth)
    {
        var dateString = $"{expiryMonth.ToString()}/{expiryYear.ToString()[^2..]}";

        return expiryMonth < 10
            ? $"0{dateString}"
            : dateString;
    }
}