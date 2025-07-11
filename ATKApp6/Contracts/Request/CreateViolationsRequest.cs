using System.Runtime.Serialization;
using ATKApp6.Domain.Enums;

namespace ATKApp6.Contracts.Request
{
    public record CreateViolationsRequest(Dictionary<Violations, ViolationDTO> Violations);

    
}