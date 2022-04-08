using Microsoft.AspNetCore.Mvc;

namespace LocalBus.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
