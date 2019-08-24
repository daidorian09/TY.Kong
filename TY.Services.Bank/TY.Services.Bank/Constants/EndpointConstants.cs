namespace TY.Services.Bank.Constants
{
    public static class EndpointConstants
    {
        #region Accounts

        public const string AccountsBaseUrl = "http://localhost:7001/api/accounts";
        public const int AccountsEndpointTimeout = 3000;

        #endregion

        #region Transactions

        public const string TransactionsBaseUrl = "http://localhost:7001/api/transaction";
        public const int TransactionsEndpointTimeout = 3000;

        #endregion
    }
}
