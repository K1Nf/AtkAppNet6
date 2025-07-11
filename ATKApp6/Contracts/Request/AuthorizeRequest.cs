using ATKApp6.Domain.Enums;
using ATKApp6.Domain.Models;
using System.Text.Json.Serialization;

namespace ATKApp6.Contracts.Request
{
    public record AuthorizeRequest(StructuredOrganizations OrganizationName, string Password);
}