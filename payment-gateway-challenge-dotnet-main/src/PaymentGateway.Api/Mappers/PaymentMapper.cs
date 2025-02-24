using PaymentGateway.Api.Entities;
using PaymentGateway.Api.Enums;
using PaymentGateway.Api.Models.Responses;

namespace PaymentGateway.Api.Mappers;

public static class PaymentMapper
{
    public static Payment ToPayment(this PaymentResponse paymentResponse)
    {
        return new Payment
        {
            Id = paymentResponse.Id,
            Status = Enum.Parse<PaymentStatus>(paymentResponse.Status),
            CardNumberLastFour = paymentResponse.CardNumberLastFour,
            ExpiryMonth = paymentResponse.ExpiryMonth,
            ExpiryYear = paymentResponse.ExpiryYear,
            Currency = Enum.Parse<CurrencyCode>(paymentResponse.Currency),
            Amount = paymentResponse.Amount
        };
    }
}