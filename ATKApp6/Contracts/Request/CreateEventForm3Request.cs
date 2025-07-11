using ATKApp6.Domain.Enums;
using ATKApp6.Domain.Models;
using System.Diagnostics.Eventing.Reader;

namespace ATKApp6.Contracts.Request
{
    public record CreateEventForm3Request(string? Name, string? Content, string Actor, 
                                          string? Date, string ThemeCode,
                                          CreateViolationsRequest CreateViolationsRequest);
}
