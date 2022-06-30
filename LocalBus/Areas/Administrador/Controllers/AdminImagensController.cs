using Microsoft.AspNetCore.Mvc;

namespace LocalBus.Areas.Administrador.Controllers
{
    public class AdminImagensController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
