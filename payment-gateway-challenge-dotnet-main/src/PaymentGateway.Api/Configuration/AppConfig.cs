namespace PaymentGateway.Api.Configuration;

/// <summary>
/// This setting should be in the app settings file as it will vary between environments.
/// For testing it can point to localhost.
/// For small applications I like a single, simple config class like this.
/// </summary>
public class AppConfig
{
    public string AcquiringBankBaseUrl { get; init; } = null!;
}