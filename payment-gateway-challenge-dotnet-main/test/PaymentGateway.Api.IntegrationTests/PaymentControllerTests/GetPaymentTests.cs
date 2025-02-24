using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Api.Constants;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Api.IntegrationTests.Helpers.Fakes;
using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Api.Services;
using System.Net;
using System.Net.Http.Json;

namespace PaymentGateway.Api.IntegrationTests.PaymentControllerTests;

public class GetPaymentTests
{
    private readonly Random _random = new();
    private readonly HttpClient _client;
    private readonly PaymentsRepository _paymentsRepository;

    public GetPaymentTests()
    {
        var webApplicationFactory = new WebApplicationFactory<PaymentsController>();
        _paymentsRepository = new PaymentsRepository();
        _client = 
            webApplicationFactory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services => 
                        ((ServiceCollection)services).AddSingleton(_paymentsRepository)))
                .CreateClient();
    }
    
    [Fact]
    public async Task GetPayment_RetrievesAPaymentSuccessfully()
    {
        // Arrange
        var payment = FakePayment.CreateValid(_random); 
        _paymentsRepository.Add(payment);
        
        // Act
        var response = await _client.GetAsync($"{Urls.PaymentsPath}/{payment.Id}");
        var paymentResponse = await response.Content.ReadFromJsonAsync<PaymentResponse>();
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(paymentResponse);
        Assert.Equal(paymentResponse!.CardNumberLastFour, payment.CardNumberLastFour);
        Assert.Equal(paymentResponse.Id, payment.Id);
        Assert.Equal(paymentResponse.ExpiryMonth, payment.ExpiryMonth);
        Assert.Equal(paymentResponse.ExpiryYear, payment.ExpiryYear);
        Assert.Equal(paymentResponse.Currency, payment.Currency.ToString());
        Assert.Equal(paymentResponse.Amount, payment.Amount);
        Assert.Null(paymentResponse.Errors);
        Assert.Equal(paymentResponse.Status, payment.Status.ToString());
    }

    [Fact]
    public async Task GetPayment_Returns404IfPaymentNotFound()
    {
        // Act
        var response = await _client.GetAsync($"{Urls.PaymentsPath}/{Guid.NewGuid()}");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}