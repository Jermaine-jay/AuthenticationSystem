using Microsoft.AspNetCore.Authorization;

namespace AuthenticationSystem.PolicyAuthorization
{
    public class PolicyRequirementHandler : AuthorizationHandler<PolicyRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PolicyRequirementHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            var endpoint = _httpContextAccessor.HttpContext.GetEndpoint();
            var endpointName = endpoint?.Metadata.GetMetadata<EndpointNameMetadata>()?.EndpointName;
            requirement.Status = endpointName;
            var actionClaim = context.User.Claims.Where(x => x.Type.Equals("permissions", StringComparison.OrdinalIgnoreCase)).ToList();
            if (actionClaim.Count < 1 || !actionClaim.Any(x => x.Value.Contains(requirement.Status)))
            {
                throw new UnauthorizedAccessException("User is not authorized to access this resource.");
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
