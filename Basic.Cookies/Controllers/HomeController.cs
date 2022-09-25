using Basic.Cookies.CustomAuthProvider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Basic.Cookies.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [Rank(3)]
        public IActionResult Rank()
        {
            return RedirectToAction("Secret");
        }

        [SecurityLevel(7)]
        public IActionResult Security()
        {
            return RedirectToAction("Secret");
        }

        public IActionResult Authenticate()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"dd"),
                new Claim(ClaimTypes.Email,"test@test.com"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"Admin2"),
                new Claim(DynamicPolicies.SecurityLevel,"9"),
                new Claim(DynamicPolicies.Rank,"2"),
            };

            var identity = new ClaimsIdentity(claims, "principal");

            var userPrincipal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}
