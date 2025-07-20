using Microsoft.AspNetCore.Authorization;

namespace ATKApp6.Infrastructure.Extensions.Authorization
{
    public class PolicyNameRequirements : IAuthorizationRequirement
    {
        public string PolicyName;
        public PolicyNameRequirements(string policyName)
        {
            PolicyName = policyName;
        }
    }
}
