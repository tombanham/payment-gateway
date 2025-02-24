namespace PaymentGateway.Api.IntegrationTests.Helpers;

public static class Constants
{
    public static class Time
    {
        public static readonly DateTimeOffset FakeUtcNow = new(2000, 2, 1, 1, 1, 1, TimeSpan.Zero);
    }
    
    public static class MediaTypes
    {
        public const string ApplicationJson = "application/json";
    }
}