using LocalBus.Models;
using Microsoft.AspNetCore.Mvc;
using static LocalBus.Models.Ponto;

namespace LocalBus.Controllers
{
    public class AlunoController : Controller
    {
       

        public IActionResult Index()
        {
            var listadePontos = new EscolaPonto();
           

            return View();
        }
    }
}
