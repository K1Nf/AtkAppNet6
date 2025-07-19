namespace ATKApp6.Infrastructure.Extensions
{
    public interface IPasswordHash
    {
        public string HashPassword(string password);
        public bool VerifyPassword(char[] password, string hashedPassword);
    }
}