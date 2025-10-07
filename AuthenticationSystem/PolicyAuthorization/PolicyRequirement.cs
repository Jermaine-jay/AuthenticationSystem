using Microsoft.AspNetCore.Authorization;

namespace AuthenticationSystem.PolicyAuthorization
{
    public class PolicyRequirement : IAuthorizationRequirement
    {
        public string Status { get; set; }
        public PolicyRequirement(string status)
        {
            Status = status;
        }
        public PolicyRequirement()
        {
            
        }
    }
}
