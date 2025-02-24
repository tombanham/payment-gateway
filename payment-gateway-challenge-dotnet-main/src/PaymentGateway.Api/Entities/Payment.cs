using PaymentGateway.Api.Enums;

namespace PaymentGateway.Api.Entities;

/// <summary>
/// Not too necessary for such a small application, but this helps decouple the data we store from the data contracts.
/// For larger applications I might also add value objects for these properties.
/// </summary>
public class Payment
{
    public required Guid Id { get; init; }
    public required PaymentStatus Status { get; init; }
    public required string CardNumberLastFour { get; init; }
    public required int ExpiryMonth { get; init; }
    public required int ExpiryYear { get; init; }
    public required CurrencyCode Currency { get; init; }
    public required int Amount { get; init; }
}