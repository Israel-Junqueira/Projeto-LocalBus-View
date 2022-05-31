using Microsoft.AspNetCore.Mvc;

namespace LocalBus.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class AdministradorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
