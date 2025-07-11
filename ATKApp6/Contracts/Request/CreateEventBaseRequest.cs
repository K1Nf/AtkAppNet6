namespace ATKApp6.Contracts.Request
{
    public record CreateEventBaseRequest(string? Name, string Actor, string Content, 
                                        string? Date, string ThemeCode,
                                        CreateParticipantsRequest? CreateParticipantsRequest,
                                        CreateMediaLinkRequest? CreateMediaLinkRequest);

}
