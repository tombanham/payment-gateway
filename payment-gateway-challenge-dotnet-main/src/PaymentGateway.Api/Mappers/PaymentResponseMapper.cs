using PaymentGateway.Api.Entities;
using PaymentGateway.Api.Models.Responses;

namespace PaymentGateway.Api.Mappers;

public static class PaymentResponseMapper
{
    public static PaymentResponse ToPaymentResponse(this Payment payment)
    {
        return new PaymentResponse
        {
            Id = payment.Id,
            Status = payment.Status.ToString(),
            CardNumberLastFour = payment.CardNumberLastFour,
            ExpiryMonth = payment.ExpiryMonth,
            ExpiryYear = payment.ExpiryYear,
            Currency = payment.Currency.ToString(),
            Amount = payment.Amount,
            Errors = null
        };
    }
}