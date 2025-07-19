using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace ATKApp6.Infrastructure.Extensions
{
    public class PasswordHasher : IPasswordHash
    {
        public string HashPassword(string stringPassword)
        {
            char[] password = stringPassword.ToCharArray();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            try
            {
                var passwordString = Encoding.UTF8.GetString(passwordBytes);
                return BCrypt.Net.BCrypt.EnhancedHashPassword(passwordString, HashType.SHA384);
            }
            finally
            {
                Array.Clear(password, 0, password.Length);
                Array.Clear(passwordBytes, 0, passwordBytes.Length);
            }
        }


        public bool VerifyPassword(char[] charPassword, string hashedPassword)
        {
            byte[] pwdBytes = Encoding.UTF8.GetBytes(charPassword);

            try
            {
                string securePwd = Encoding.UTF8.GetString(pwdBytes);
                return BCrypt.Net.BCrypt.EnhancedVerify(securePwd, hashedPassword, HashType.SHA384);
            }
            finally
            {
                Array.Clear(pwdBytes, 0, pwdBytes.Length);
            }
        }
    }
}
