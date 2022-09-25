using Microsoft.AspNetCore.Authorization;

namespace Basic.Cookies.Requirements
{
    public class CustomRequireClaim : IAuthorizationRequirement
    {
        public string claimType { get; set; }

        public CustomRequireClaim(string claimType)
        {
            this.claimType = claimType;
        }
    }

    public class ClaimHandler : AuthorizationHandler<CustomRequireClaim>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequireClaim requirement)
        {
            var hasClaim = context.User.Claims.Any(x => x.Type == requirement.claimType);
            if (hasClaim) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public static class AuthorizationExtensions
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(
            this AuthorizationPolicyBuilder builder,
            string claimType
            )
        {
            builder.AddRequirements(new CustomRequireClaim(claimType));
            return builder;
        }
    }
}
