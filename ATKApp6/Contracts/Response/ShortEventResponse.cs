using ATKApp6.Domain.Enums;
using ATKApp6.Domain.Models;

namespace ATKApp6.Contracts.Response
{
    public record ShortEventResponse
    {
        public Guid Id { get; init; }
        public string ThemeCode { get; init; }
        public string? Name { get; init; }
        public string? Date { get; init; }
        public int? ParticipantsCount { get; init; }
        public string? Content { get; init; }
        public string[]? Links { get; init; }
        //public string Actor { get; init; }
        public StructuredOrganizations OrganizerName { get; init; }
    }
}
