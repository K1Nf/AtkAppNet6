using ATKApp6.Domain.Enums;
using ATKApp6.Domain.Models;
using System.Diagnostics.Eventing.Reader;

namespace ATKApp6.Contracts.Request
{
    public record CreateEventForm2Request(string Name, string Content, string Actor, string Date,
                                          string ThemeCode, string Request, string Description,
                                          string ResultDescription, string Participant,
                                          CreateMediaLinkRequest? CreateMediaLinkRequest,
                                          CreateParticipantsRequest? CreateParticipantsRequest);
}