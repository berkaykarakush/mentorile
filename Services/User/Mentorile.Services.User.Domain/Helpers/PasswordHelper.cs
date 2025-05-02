using System.Security.Cryptography;
using System.Text;

namespace Mentorile.Services.User.Domain.Helpers;
public static class PasswordHelper
{
    // Sifreyi hashler
    public static string HashPassword(string password)
    {
        using(var sha256 = SHA256.Create())
        {
            var salt = GenerateSalt();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltedPassword = Combine(passwordBytes, salt);
            // SHA-256 hashleme islemi
            var hash = sha256.ComputeHash(saltedPassword);
            // Salt ve hash'i birlikte donduruyoruz
            return Convert.ToBase64String(Combine(hash, salt));
        }
    }

    // sifreyi dogrular
    public static bool VerifyPassword(string storedHash, string password)
    {
        var storedBytes = Convert.FromBase64String(storedHash);
        var hashLength = 32; // sha-256 hash uzunlugu
        var salt = new byte[storedBytes.Length - hashLength];
        var hash = new byte[hashLength];

        Array.Copy(storedBytes, 0, hash, 0,hashLength);
        Array.Copy(storedBytes, hashLength, salt, 0, salt.Length);

        // Hashleme islemi
        using(var sha256 = SHA256.Create())
        {
            var saltedPassword = Combine(Encoding.UTF8.GetBytes(password), salt);
            var computedHash = sha256.ComputeHash(saltedPassword);
            // elde edilen hash ile stored hash karsilastirilir
            return AreHashesEqual(computedHash, hash);
        }
    } 

    private static byte[] GenerateSalt()
    {
        var salt = new byte[16]; // salt uzunlugu
        using(var rng = new RNGCryptoServiceProvider()) rng.GetBytes(salt);
        return salt;
    }

    private static byte[] Combine(byte[] first, byte[] second)
    {
        var combined = new byte[first.Length + second.Length];
        Array.Copy(first, 0, combined, 0, first.Length);
        Array.Copy(second, 0, combined, first.Length, second.Length);
        return combined;
    }

    private static bool AreHashesEqual(byte[] hash1, byte[] hash2)
    {
        if(hash1.Length != hash2.Length) return false;

        for(int i = 0; i < hash1.Length; i++)
            if(hash1[i] != hash2[i]) return false;

        return true;
    }
}