namespace PaymentGateway.Api.Models.Requests;

public record PostPaymentRequest
{
    // This was originally last four only, but the specification says it should be 14-19 long.
    // I've changed this to be the full number to match and also changed the type to string which seems more appropriate.
    public required string CardNumber { get; init; }
    public required int ExpiryMonth { get; init; }
    public required int ExpiryYear { get; init; }
    public required string Currency { get; init; }
    public required int Amount { get; init; }
    // Cvv should also be a string rather than an int, these aren't numbers.
    public required string Cvv { get; init; }
}