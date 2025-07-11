using ATKApp6.Domain.Enums;

namespace ATKApp6.Contracts.Request
{
    public record CreateSupportRequest(string? Supported, List<SupportDto> Supports);

    public class SupportDto
    {
        public SupportType Key { get; set; }
        public string? Description {get; set;}
    }
}