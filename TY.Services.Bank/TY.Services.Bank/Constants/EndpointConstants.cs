﻿namespace TY.Services.Bank.Constants
{
    public static class EndpointConstants
    {
        #region Accounts

        public const string AccountsBaseUrl = "http://ty-persistent-bank:7001/api/accounts/";
        public const int AccountsEndpointTimeout = 3000;

        #endregion

        #region Transactions

        public const string TransactionsBaseUrl = "http://ty-persistent-bank:7001/api/transactions/";
        public const int TransactionsEndpointTimeout = 3000;

        #endregion
    }
}
