using ATKApp6.Domain.Enums;

namespace ATKApp6.Infrastructure.Extensions
{
    public interface IJwtProvider
    {
        public string CreateNewToken(Guid userId);
    }
}
