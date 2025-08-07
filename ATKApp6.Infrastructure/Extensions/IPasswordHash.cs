namespace ATKApp6.Infrastructure.Extensions
{
    public interface IPasswordHash
    {
        public string HashPassword(char[] pass);
        public bool VerifyPassword(char[] password, string hashedPassword);
    }
}