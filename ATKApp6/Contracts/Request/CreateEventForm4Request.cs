using ATKApp6.Domain.Enums;
using ATKApp6.Domain.Models;
using System.Diagnostics.Eventing.Reader;

namespace ATKApp6.Contracts.Request
{
    public record CreateEventForm4Request(string Name, string Content, string Actor, 
                                          string Date, bool DirectToNAC, 
                                          string? SendToSubjects, string ThemeCode,
                                          CreateAgreementRequest? CreateAgreementRequest,
                                          CreateMediaLinkRequest? CreateMediaLinkRequest,
                                          CreateParticipantsRequest? CreateParticipantsRequest
       );

    
}
