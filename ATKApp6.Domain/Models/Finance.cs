using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATKApp6.Domain.Models
{
    public class Finance
    {
        public Finance() {}

        public Finance(int? municipalBudget, int? regionalBudget, int? granteBudget, int? otherBudget, string? description, Guid eventId)
        {
            Id = Guid.NewGuid();
            MunicipalBudget = municipalBudget;
            RegionalBudget = regionalBudget;
            GranteBudget = granteBudget;
            OtherBudget = otherBudget;
            Description = description;
            EventId = eventId;
            Total = municipalBudget + regionalBudget + granteBudget + otherBudget;
        }


        public Guid Id { get; init; }
        public int? MunicipalBudget { get; set; } 
        public int? RegionalBudget { get; set; } 
        public int? GranteBudget { get; set; } 
        public int? OtherBudget { get; set; } 

        [NotMapped]
        public int? Total { get; set; }
        public string? Description { get; set; }
        


        [Newtonsoft.Json.JsonIgnore]
        public EventForm1? Event { get; set; }
        public Guid EventId { get; set; }
    }
}
