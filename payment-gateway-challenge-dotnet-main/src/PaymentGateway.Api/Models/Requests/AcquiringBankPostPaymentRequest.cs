using System.Text.Json.Serialization;

namespace PaymentGateway.Api.Models.Requests;

/// <summary>
/// For larger applications I'd split Infrastructure services and models into their own project.
/// </summary>
public class AcquiringBankPostPaymentRequest
{
    [JsonPropertyName("card_number")]
    public required string CardNumber { get; init; }
    
    [JsonPropertyName("expiry_date")]
    public required string ExpiryDate { get; init; }
    
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }
    
    [JsonPropertyName("amount")]
    public required int Amount { get; init; }
    
    [JsonPropertyName("cvv")]
    public required string Cvv { get; init; }
}