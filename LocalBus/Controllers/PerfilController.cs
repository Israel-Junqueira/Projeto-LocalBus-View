using Microsoft.AspNetCore.Mvc;

namespace LocalBus.Controllers
{
    public class PerfilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
