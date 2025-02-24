using Microsoft.Extensions.Options;
using PaymentGateway.Api.Configuration;
using PaymentGateway.Api.Constants;
using PaymentGateway.Api.Exceptions;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Responses;
using System.Text.Json;

namespace PaymentGateway.Api.Services;

public class AcquiringBank(IOptions<AppConfig> appConfig, HttpClient httpClient)
{
    /// <summary>
    /// Throws an exception if the call to the Acquiring Bank returns an error or a response
    /// that can't be deserialized.
    /// These exceptions result in a BadGateway response to the merchant. 
    /// </summary>
    public async Task<AcquiringBankPostPaymentResponse> PostPaymentAsync(
        AcquiringBankPostPaymentRequest acquiringBankPostPaymentRequest,
        CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsync(
            $"{appConfig.Value.AcquiringBankBaseUrl}/{Urls.AcquiringBankPaymentsPath}",
            new StringContent(JsonSerializer.Serialize(acquiringBankPostPaymentRequest)), 
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return
            await response.Content.ReadFromJsonAsync<AcquiringBankPostPaymentResponse>(cancellationToken)
            ?? throw new BadGatewayException();
    }
}