namespace ATKApp6.Contracts.Request
{
    public record CreateSourcesOfDistributionRequest(List<SourceOfDestinationDTO> Links);

    public class SourceOfDestinationDTO
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
}