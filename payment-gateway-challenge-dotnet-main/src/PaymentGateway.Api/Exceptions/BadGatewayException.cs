namespace PaymentGateway.Api.Exceptions;

public class BadGatewayException : Exception
{
    public BadGatewayException() { }
    public BadGatewayException(string message) : base(message) { }
    public BadGatewayException(string message, Exception innerException) : base(message, innerException) { }
}