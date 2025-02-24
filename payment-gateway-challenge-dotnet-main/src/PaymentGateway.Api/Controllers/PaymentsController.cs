using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Constants;
using PaymentGateway.Api.Enums;
using PaymentGateway.Api.Mappers;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Api.Services;

namespace PaymentGateway.Api.Controllers;

[Route(Urls.PaymentsPath)]
[ApiController]
public class PaymentsController(
    IValidator<PostPaymentRequest> postPaymentRequestValidator,
    PaymentsRepository paymentsRepository, 
    AcquiringBank acquiringBank) : Controller
{
    [HttpGet("{id:guid}")]
    public ActionResult<PaymentResponse?> Get(Guid id)
    {
        var payment = paymentsRepository.Get(id);

        if (payment is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(payment.ToPaymentResponse());
    }
    
    /// <summary>
    /// Validate the request, call the acquiring bank, save the payment then return the response.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PaymentResponse?>> PostAsync(
        PostPaymentRequest postPaymentRequest,
        CancellationToken cancellationToken)
    {
        var validationResult = 
            await postPaymentRequestValidator.ValidateAsync(
                postPaymentRequest, 
                cancellationToken);

        if (!validationResult.IsValid)
        {
            return CreateBadRequestObjectResult(postPaymentRequest, validationResult);
        }
        
        var bankResponse = 
            await acquiringBank.PostPaymentAsync(
                postPaymentRequest.ToAcquiringBankRequest(),
                cancellationToken);
    
        var paymentResponse =
            new PaymentResponse
            {
                Id = Guid.NewGuid(),
                Status = bankResponse.Authorized ? PaymentStatus.Authorized.ToString() : PaymentStatus.Declined.ToString(),
                CardNumberLastFour = postPaymentRequest.CardNumber[^4..],
                ExpiryMonth = postPaymentRequest.ExpiryMonth,
                ExpiryYear = postPaymentRequest.ExpiryYear,
                Currency = postPaymentRequest.Currency,
                Amount = postPaymentRequest.Amount,
                Errors = null
            };

        paymentsRepository.Add(paymentResponse.ToPayment());

        return new CreatedResult($"{Urls.PaymentsPath}/{paymentResponse.Id}", paymentResponse);
    }

    /// <summary>
    /// There's a rejected status in the enum but the specification suggests that it is not returned if validation
    /// is successful. I've interpreted this as meaning we can return the same response body if validation is
    /// unsuccessful, except we don't save the payment.
    /// 'Created' seemed misleading in this case so I return 'BadRequest' instead,
    /// it was also easy to add an errors property as I already have the validation result from fluent validation.
    /// </summary>
    private static ActionResult<PaymentResponse?> CreateBadRequestObjectResult(
        PostPaymentRequest postPaymentRequest,
        ValidationResult validationResult)
    {
        return new BadRequestObjectResult(
            new PaymentResponse
            {
                Id = Guid.NewGuid(),
                Status = PaymentStatus.Rejected.ToString(),
                CardNumberLastFour = postPaymentRequest.CardNumber[^4..],
                ExpiryMonth = postPaymentRequest.ExpiryMonth,
                ExpiryYear = postPaymentRequest.ExpiryYear,
                Currency = postPaymentRequest.Currency,
                Amount = postPaymentRequest.Amount,
                Errors = validationResult.ToString()
            });
    }
}