using ATKApp6.Domain.Enums;
using System.Runtime.Serialization;

namespace ATKApp6.Contracts.Request
{
    public record CreateFeedBackRequest(string? Description, FeedBackTypes[] FeedBackTypes);
}