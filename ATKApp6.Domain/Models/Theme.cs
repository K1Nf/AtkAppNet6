using System.Text.Json.Serialization;

namespace ATKApp6.Domain.Models
{
    public class Theme
    {
        public Guid Id { get; init; }
        public string Code { get; set; } = string.Empty;
        public int Form { get; set; }
        public string Description { get; set; } = string.Empty;

        [Newtonsoft.Json.JsonIgnore]
        public List<EventBase> Events { get; set; } = new List<EventBase>();
    }
}
