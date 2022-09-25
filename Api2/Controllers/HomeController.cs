using Microsoft.AspNetCore.Mvc;

namespace Api2.Controllers
{
    public class HomeController : Controller
    {
        [Route("/secret2")]
        public string Index()
        {
            return "call from api 2";
        }
    }
}
