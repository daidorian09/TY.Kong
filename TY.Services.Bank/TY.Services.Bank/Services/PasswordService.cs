using System;
using System.Security.Cryptography;
using TY.Services.Bank.Services.Interfaces;

namespace TY.Services.Bank.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        private const int SaltLength = 32;
        public string GenerateSalt()
        {
            var salt = new byte[SaltLength];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return Convert.ToBase64String(salt);;
        }

        public string HashPassword(string salt, string password)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);

                foreach (var b in hashedInputBytes) 
                {
                     hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString();
            }
        }

        public bool ValidatePassword(string hashedPassword, string password)
        {
            return hashedPassword.Equals(password);
        }
    }
}