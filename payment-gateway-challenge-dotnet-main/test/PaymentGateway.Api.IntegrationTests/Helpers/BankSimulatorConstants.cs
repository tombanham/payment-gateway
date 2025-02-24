namespace PaymentGateway.Api.IntegrationTests.Helpers;

internal static class BankSimulatorConstants
{
    /// <summary>
    /// Use this CardNumberLastFour value for the bank simulator to return...
    /// </summary>
    internal static class CardNumber
    {
        /// <summary>
        /// Authorized.
        /// </summary>
        internal const string AuthorizedResponse = "11111111111111";
    
        /// <summary>
        /// Declined.
        /// </summary>
        internal const string DeclinedResponse = "22222222222222";

        /// <summary>
        /// 503.
        /// </summary>
        internal const string AcquiringBankUnavailable = "10000000000000";
    }
}