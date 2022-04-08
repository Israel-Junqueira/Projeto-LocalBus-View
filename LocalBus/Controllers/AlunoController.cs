using Microsoft.AspNetCore.Mvc;

namespace LocalBus.Controllers
{
    public class AlunoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
