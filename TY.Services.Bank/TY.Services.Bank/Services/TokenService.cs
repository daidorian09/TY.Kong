using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TY.Services.Bank.Models.Account;
using TY.Services.Bank.Models.Configuration;
using TY.Services.Bank.Services.Interfaces;

namespace TY.Services.Bank.Services
{
    public class TokenService : ITokenService
    {
        #region Fields

        private readonly JwtConfiguration _jwtConfiguration;

        #endregion

        #region Ctor

        public TokenService(IOptions<JwtConfiguration> jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration.Value;
        }

        #endregion
        public string GenerateJwtToken(Account account)
        {
            var claim = new[]
            {
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.GivenName, account.FirstName),
                new Claim(ClaimTypes.Surname, account.LastName),
                new Claim(ClaimTypes.MobilePhone, account.PhoneNumber),
                new Claim(ClaimTypes.NameIdentifier, account.Id),
                new Claim(ClaimTypes.StreetAddress, account.Address),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(_jwtConfiguration.Issuer,
                _jwtConfiguration.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_jwtConfiguration.AccessExpiration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}