namespace ATKApp6.Contracts.Request
{
    public record CreateFinanceRequest(int? MunicipalBudget, int? RegionalBudget, int? GranteBudget, int? OtherBudget, string? Description);
}
