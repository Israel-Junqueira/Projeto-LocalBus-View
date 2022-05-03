using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static LocalBus.Models.Ponto;

namespace LocalBus.Controllers
{
    public class AlunoController : Controller
    {

        private readonly IPontoRepository _context;

        public AlunoController(IPontoRepository context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Pontos = _context.Pontosrepository;
           
            return View(Pontos);
        }


    }
}
