namespace PaymentGateway.Api.Models.Responses;

/// <summary>
/// I renamed this to just 'PaymentResponse' as this model can be the same for both Get and Post.
/// </summary>
public class PaymentResponse
{
    public required Guid Id { get; init; }
    // I changed this to a string so that it doesn't get returned as a number
    public required string Status { get; init; }
    // this was originally an int, but I think string is more appropriate for card numbers
    public required string CardNumberLastFour { get; init; }
    public required int ExpiryMonth { get; init; }
    public required int ExpiryYear { get; init; }
    public required string Currency { get; init; }
    public required int Amount { get; init; }

    /// <summary>
    /// Extra property to return validation errors (outside of spec but was quick to add)
    /// </summary>
    public required string? Errors { get; init; }
}
