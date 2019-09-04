using System;

namespace TY.Services.Bank.Internal
{
    public static class StringExtension
    {
        public static string ParseBearerFromHeader(this string token)
        {
            var authorizationToken = token.Split(null as char[], StringSplitOptions.RemoveEmptyEntries);

            if (authorizationToken.Length != 2)
            {
                return null;
            }

            return authorizationToken[1];
        }
    }
}