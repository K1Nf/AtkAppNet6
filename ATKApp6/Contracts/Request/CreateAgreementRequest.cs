using ATKApp6.Domain.Enums;

namespace ATKApp6.Contracts.Request
{
    public record CreateAgreementRequest(List<AgreementRequest> Agreements);

    public class AgreementRequest
    {
        public string Name { get; set; }
        public ResultEnum Result { get; set; }
        public string? Description { get; set; }
    }
}
