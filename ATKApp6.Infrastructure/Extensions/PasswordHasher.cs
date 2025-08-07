using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace ATKApp6.Infrastructure.Extensions
{
    public class PasswordHasher : IPasswordHash
    {
        private const int SaltSize = 16; // 128 бит
        private const int HashSize = 32; // 256 бит
        private const int Iterations = 100_000; // рекомендуется 100k+

        public string HashPassword(char[] passwordChars)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(passwordChars);

            try
            {
                using var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, Iterations, HashAlgorithmName.SHA512);
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Конкатенируем salt + hash для хранения
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
                Buffer.BlockCopy(hash, 0, hashBytes, SaltSize, HashSize);

                return Convert.ToBase64String(hashBytes);
            }
            finally
            {
                Array.Clear(passwordChars, 0, passwordChars.Length);
                Array.Clear(passwordBytes, 0, passwordBytes.Length);
            }
        }



        public bool VerifyPassword(char[] passwordChars, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Разделяем salt и хеш
            byte[] salt = new byte[SaltSize];
            byte[] storedPasswordHash = new byte[HashSize];

            Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);
            Buffer.BlockCopy(hashBytes, SaltSize, storedPasswordHash, 0, HashSize);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(passwordChars);

            try
            {
                using var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, Iterations, HashAlgorithmName.SHA512);
                byte[] computedHash = pbkdf2.GetBytes(HashSize);

                // Сравнение без timing attacks
                return CryptographicOperations.FixedTimeEquals(storedPasswordHash, computedHash);
            }
            finally
            {
                Array.Clear(passwordBytes, 0, passwordBytes.Length);
            }
        }
    }
}
