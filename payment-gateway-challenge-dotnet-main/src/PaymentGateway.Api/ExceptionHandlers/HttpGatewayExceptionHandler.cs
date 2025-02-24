using System.Net;

using Microsoft.AspNetCore.Diagnostics;

using PaymentGateway.Api.Exceptions;

namespace PaymentGateway.Api.ExceptionHandlers;

/// <summary>
/// When a dependency returns something unexpected we can return 502.
/// </summary>
internal class BadGatewayExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not BadGatewayException)
        {
            return ValueTask.FromResult(false);
        }

        httpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;

        return ValueTask.FromResult(true);
    }
}