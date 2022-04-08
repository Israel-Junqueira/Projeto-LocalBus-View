using Microsoft.AspNetCore.Mvc;

namespace LocalBus.Controllers
{
    public class AdministracaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
