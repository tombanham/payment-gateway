using System.Net;

using Microsoft.AspNetCore.Diagnostics;

namespace PaymentGateway.Api.ExceptionHandlers;

/// <summary>
/// When a dependency is unavailable we can return 502.
/// </summary>
internal class HttpRequestExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not HttpRequestException { StatusCode: HttpStatusCode.ServiceUnavailable })
        {
            return ValueTask.FromResult(false);
        }

        httpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;

        return ValueTask.FromResult(true);
    }
}