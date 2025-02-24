using Moq;
using PaymentGateway.Api.IntegrationTests.Helpers.Fakes;
using PaymentGateway.Api.Validators;
using Xunit;

namespace PaymentGateway.Api.UnitTests;

/// <summary>
/// Unit tests for the validator as I'd like to cover the boundary for each validation rule.
/// I usually make these unit tests rather than integration tests as there will typically be more of them
/// and they run much slower as integration tests.
/// </summary>
public class PostPaymentRequestValidatorTests
{
    private readonly PostPaymentRequestValidator _postPaymentRequestValidator;
    private readonly Random _random = new();
    private readonly Mock<TimeProvider> _timeProvider;

    public PostPaymentRequestValidatorTests()
    {
        _timeProvider = new(MockBehavior.Strict);
        _timeProvider.Setup(x => x.GetUtcNow()).Returns(IntegrationTests.Helpers.Constants.Time.FakeUtcNow);
        _postPaymentRequestValidator = new PostPaymentRequestValidator(_timeProvider.Object);
    }
    
    [Fact]
    public async Task ValidateAsync_GivenCardNumberEmpty_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {  
                CardNumber = string.Empty
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("'Card Number' must not be empty.", result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenCardNumberTooShort_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {  
                CardNumber = string.Empty.PadRight(PostPaymentRequestValidator.CardNumberMinLength - 1, '1')
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("The length of 'Card Number' must be at least 14 characters. You entered 13 characters.", result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenCardNumberTooLong_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {  
                CardNumber = string.Empty.PadRight(PostPaymentRequestValidator.CardNumberMaxLength + 1, '1')
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("The length of 'Card Number' must be 19 characters or fewer. You entered 20 characters.", result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenCardNumberContainsNonNumeric_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {  
                CardNumber = string.Empty.PadRight(PostPaymentRequestValidator.CardNumberMinLength - 1, '1') + "A"
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(PostPaymentRequestValidator.CardNumberMustBeNumericErrorMessage, result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenExpiryMonthTooSmall_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {  
                ExpiryMonth = 0
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("'Expiry Month' must be greater than or equal to '1'.", result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenExpiryMonthTooLarge_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {  
                ExpiryMonth = 13
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("'Expiry Month' must be less than or equal to '12'.", result.Errors.Select(x => x.ErrorMessage));
    }
    
    
    [Fact]
    public async Task ValidateAsync_GivenYearInThePast_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {  
                ExpiryMonth = IntegrationTests.Helpers.Constants.Time.FakeUtcNow.Month,
                ExpiryYear = IntegrationTests.Helpers.Constants.Time.FakeUtcNow.Year - 1
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(PostPaymentRequestValidator.ExpiryDateMustBeInTheFutureErrorMessage, result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenExpiryMonthPlusYearInThePast_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {  
                ExpiryMonth = IntegrationTests.Helpers.Constants.Time.FakeUtcNow.Month - 1, // Fake month is february so this is safe
                ExpiryYear = IntegrationTests.Helpers.Constants.Time.FakeUtcNow.Year
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(PostPaymentRequestValidator.ExpiryDateMustBeInTheFutureErrorMessage, result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenCurrencyCodeNotInEnum_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {  
                Currency = "ABC"
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(PostPaymentRequestValidator.CurrencyCodeMustBeIsoErrorMessage, result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ValidateAsync_GivenAmountNonPositive_ReturnsInvalid(int amount)
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            {  
                Amount = amount
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(PostPaymentRequestValidator.AmountMustBePositiveErrorMessage, result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenCVVTooShort_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            { 
                Cvv = string.Empty.PadRight(PostPaymentRequestValidator.CvvMinLength - 1, '1') 
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("The length of 'Cvv' must be at least 3 characters. You entered 2 characters.", result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenCVVTooLong_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            { 
                Cvv = string.Empty.PadRight(PostPaymentRequestValidator.CvvMaxLength + 1, '1') 
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("The length of 'Cvv' must be 4 characters or fewer. You entered 5 characters.", result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenCVVContainsNonNumeric_ReturnsInvalid()
    {
        // Arrange
        var postPaymentRequest = 
            FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object) with
            { 
                Cvv = "12A"
            };
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(PostPaymentRequestValidator.CvvMustBeNumericErrorMessage, result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Fact]
    public async Task ValidateAsync_GivenValid_ReturnsValid()
    {
        // Arrange
        var postPaymentRequest = FakePostPaymentRequest.CreateValid(_random, _timeProvider.Object);
        
        // Act
        var result = await _postPaymentRequestValidator.ValidateAsync(postPaymentRequest);
        
        // Assert
        Assert.True(result.IsValid);
    }
}