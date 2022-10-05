using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Cookies.Controllers
{
    public class OperationsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public OperationsController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, new AuthResource(),
                new OperationAuthorizationRequirement { Name = CookieJarOperations.Open});

            if (result.Succeeded) return RedirectToAction("Secret", "Home");
            
            return RedirectToAction("Index", "Home");
        }
    }

    public static class CookieJarOperations
    {
        public static string Open = "Open";
        public static string TakeCookie = "TakeCookie";
        public static string ComeNear = "ComeNear";
        public static string Look = "Look";
    }

    public class CookieJarHandler : AuthorizationHandler<OperationAuthorizationRequirement, AuthResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement, AuthResource resource)
        {
            if(requirement.Name == CookieJarOperations.Open)
            {
                if (context.User.Identity.IsAuthenticated) context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class AuthResource
    {

    }
}
