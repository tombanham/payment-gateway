using FluentValidation;
using PaymentGateway.Api.Enums;
using PaymentGateway.Api.Models.Requests;

namespace PaymentGateway.Api.Validators;

public class PostPaymentRequestValidator : AbstractValidator<PostPaymentRequest>
{
    public const string CardNumberMustBeNumericErrorMessage = "Card number must have numeric characters only";
    public const string CurrencyCodeMustBeIsoErrorMessage = "Currency must be an ISO currency code";
    public const string ExpiryDateMustBeInTheFutureErrorMessage = "Expiry date must be in the future";
    public const string AmountMustBePositiveErrorMessage = "Amount must be positive";
    public const string CvvMustBeNumericErrorMessage = "CVV must have numeric characters only";

    public const int CardNumberMinLength = 14;
    public const int CardNumberMaxLength = 19;
    
    public const int CvvMinLength = 3;
    public const int CvvMaxLength = 4;
    
    /// <summary>
    /// Regex is good for this case.
    /// I could also use it to cover other validation but I'd rather avoid regex as much as possible,
    /// MinimumLength is more readable and gives a nice error message.
    /// </summary>
    private const string NumericOnlyRegex = @"^\d+$";
    
    public PostPaymentRequestValidator(TimeProvider timeProvider)
    {
        RuleFor(x => x.CardNumber)
            .NotEmpty()
            .MinimumLength(CardNumberMinLength)
            .MaximumLength(CardNumberMaxLength)
            .Matches(NumericOnlyRegex)
            .WithMessage(CardNumberMustBeNumericErrorMessage);

        RuleFor(x => x.Currency)
            .Must(x => Enum.TryParse<CurrencyCode>(x, out _))
            .WithMessage(CurrencyCodeMustBeIsoErrorMessage);

        RuleFor(x => x.ExpiryMonth)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(12);

        RuleFor(x => x)
            .Must(x => new DateOnly(x.ExpiryYear, x.ExpiryMonth, 1) >= DateOnly.FromDateTime(timeProvider.GetUtcNow().UtcDateTime))
            .When(x => TryCreateDateOnly(x.ExpiryYear, x.ExpiryMonth))
            .WithMessage(ExpiryDateMustBeInTheFutureErrorMessage);
        
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage(AmountMustBePositiveErrorMessage);
        
        RuleFor(x => x.Cvv)
            .MinimumLength(CvvMinLength)
            .MaximumLength(CvvMaxLength)
            .Matches(NumericOnlyRegex)
            .WithMessage(CvvMustBeNumericErrorMessage);
    }

    private static bool TryCreateDateOnly(int year, int month)
    {
        try
        {
            _ = new DateOnly(year, month, 1);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}