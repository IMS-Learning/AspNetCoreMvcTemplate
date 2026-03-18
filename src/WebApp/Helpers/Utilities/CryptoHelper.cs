using System.Security.Cryptography;
using System.Text;

namespace WebApp.Helpers.Utilities;

public static class CryptoHelper
{
    private const int SALT_SIZE = 16;
    private const int HASH_SIZE = 32;
    private const int ITERATIONS = 100_000;

    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            ITERATIONS,
            HashAlgorithmName.SHA256,
            HASH_SIZE);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        var parts = hashedPassword.Split('.');
        if (parts.Length != 2)
            return false;

        var salt = Convert.FromBase64String(parts[0]);
        var expectedHash = Convert.FromBase64String(parts[1]);

        var actualHash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            ITERATIONS,
            HashAlgorithmName.SHA256,
            HASH_SIZE);

        return CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);
    }

    public static string GenerateToken(int length = 32)
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(length));
    }
}
