using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using PaymentGateway.Api.Constants;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Api.Enums;
using PaymentGateway.Api.IntegrationTests.Helpers;
using PaymentGateway.Api.IntegrationTests.Helpers.Fakes;
using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Validators;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using JsonSerializerOptions = PaymentGateway.Api.IntegrationTests.Helpers.JsonSerializerOptions;

namespace PaymentGateway.Api.IntegrationTests.PaymentControllerTests;

public class PostPaymentTests
{
    private readonly Random _random = new();
    private readonly HttpClient _client;
    private readonly Mock<TimeProvider> _timeProvider;

    public PostPaymentTests()
    {
        _timeProvider = new Mock<TimeProvider>(MockBehavior.Strict);
        _timeProvider.Setup(x => x.GetUtcNow()).Returns(IntegrationTests.Helpers.Constants.Time.FakeUtcNow);
        var webApplicationFactory = 
            new WebApplicationFactory<PaymentsController>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.Replace(ServiceDescriptor.Scoped<TimeProvider>(_ => _timeProvider.Object));
                    });
                });
        _client = webApplicationFactory.CreateClient();
    }
    
    [Fact]
    public async Task PostPaymentAsync_GivenPaymentAuthorized_ReturnsAuthorizedResponse()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {
                CardNumber = BankSimulatorConstants.CardNumber.AuthorizedResponse
            };
        
        // Act
        var response = await _client.PostAsync(
            Urls.PaymentsPath, 
            new StringContent(
                JsonSerializer.Serialize(postPaymentRequest), 
                Encoding.UTF8, 
                Helpers.Constants.MediaTypes.ApplicationJson));
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var responseObject = 
            await response.Content.ReadFromJsonAsync<PaymentResponse>(JsonSerializerOptions.CamelCaseOptions);
        AssertPostPaymentCreatedResponse(responseObject, postPaymentRequest, PaymentStatus.Authorized);
    }

    [Fact]
    public async Task PostPaymentAsync_GivenPaymentDeclined_ReturnsDeclinedResponse()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {
                CardNumber = BankSimulatorConstants.CardNumber.DeclinedResponse
            };
        
        // Act
        var response = await _client.PostAsync(
            Urls.PaymentsPath, 
            new StringContent(
                JsonSerializer.Serialize(postPaymentRequest), 
                Encoding.UTF8, 
                Helpers.Constants.MediaTypes.ApplicationJson));
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var responseObject = 
            await response.Content.ReadFromJsonAsync<PaymentResponse>(JsonSerializerOptions.CamelCaseOptions);
        AssertPostPaymentCreatedResponse(responseObject, postPaymentRequest, PaymentStatus.Declined);
    }
    
    [Fact]
    public async Task PostPaymentAsync_GivenAcquiringBankUnavailable_ReturnsBadGatewayResponse()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {
                CardNumber = BankSimulatorConstants.CardNumber.AcquiringBankUnavailable
            };
        
        // Act
        var response = await _client.PostAsync(
            Urls.PaymentsPath, 
            new StringContent(
                JsonSerializer.Serialize(postPaymentRequest), 
                Encoding.UTF8, 
                Helpers.Constants.MediaTypes.ApplicationJson));
        
        // Assert
        Assert.Equal(HttpStatusCode.BadGateway, response.StatusCode);
    }

    [Fact] 
    public async Task PostPaymentAsync_GivenInvalidRequest_ReturnsRejectedResponse()
    {
        // Arrange
        var postPaymentRequest = FakePostPaymentRequest.CreateInvalid();
        
        // Act
        var response = await _client.PostAsync(
            Urls.PaymentsPath, 
            new StringContent(
                JsonSerializer.Serialize(postPaymentRequest), 
                Encoding.UTF8, 
                Helpers.Constants.MediaTypes.ApplicationJson));
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var responseObject = await response.Content.ReadFromJsonAsync<PaymentResponse>(JsonSerializerOptions.CamelCaseOptions);
        Assert.NotNull(responseObject);
        var errors = responseObject!.Errors!.Split('\n');
        Assert.Contains(PostPaymentRequestValidator.CardNumberMustBeNumericErrorMessage, errors);
        Assert.Contains(PostPaymentRequestValidator.CurrencyCodeMustBeIsoErrorMessage, errors);
        Assert.Contains(PostPaymentRequestValidator.AmountMustBePositiveErrorMessage, errors);
        Assert.Contains(PostPaymentRequestValidator.CvvMustBeNumericErrorMessage, errors);
        Assert.Contains("The length of 'Card Number' must be at least 14 characters. You entered 7 characters.", errors);
        Assert.Contains("'Expiry Month' must be greater than or equal to '1'.", errors);
    }
    
    private static void AssertPostPaymentCreatedResponse(
        PaymentResponse? responseObject,
        PostPaymentRequest postPaymentRequest,
        PaymentStatus paymentStatus)
    {
        Assert.NotNull(responseObject);
        Assert.NotEqual(Guid.Empty, responseObject!.Id);
        Assert.Equal(postPaymentRequest.Amount, responseObject.Amount);
        Assert.Equal(postPaymentRequest.ExpiryMonth, responseObject.ExpiryMonth);
        Assert.Equal(postPaymentRequest.ExpiryYear, responseObject.ExpiryYear);
        Assert.Equal(postPaymentRequest.Currency, responseObject.Currency);
        Assert.Equal(postPaymentRequest.CardNumber[^4..], responseObject.CardNumberLastFour);
        Assert.Null(responseObject.Errors);
        Assert.Equal(paymentStatus.ToString(), responseObject.Status);
    }
}
