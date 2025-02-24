using System.Text.Json;

namespace PaymentGateway.Api.IntegrationTests.Helpers;

public static class JsonSerializerOptions
{
    public static readonly System.Text.Json.JsonSerializerOptions CamelCaseOptions = 
        new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
}