namespace TY.Services.Bank.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string GenerateSalt();
        string HashPassword(string salt, string password);
        bool ValidatePassword(string hashedPassword, string password);
    }
}