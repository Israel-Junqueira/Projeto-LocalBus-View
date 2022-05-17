using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LocalBus.ViewModels;

namespace LocalBus.Controllers
{
    public class AlunoController : Controller
    {

        private readonly IEscolaRepository _context;

        public AlunoController(IEscolaRepository context)
        {
            _context = context;
        }

        public IActionResult Index(int IdEscola)
        {

            IEnumerable<Escola> EscolasId;

            string EscolasAtuais = string.Empty;

            if (IdEscola == 0)
            {
                EscolasId = _context.EscolaRepository.OrderBy(E => E.EscolaId);

            }
            else
            {
                EscolasId = _context.EscolaRepository.Where(P => P.Escola_Ponto.Equals(IdEscola)).OrderBy(c => c.Escola_Ponto.Select(c => c.Ponto.Nome));
            }

            var EscolasListViewModel = new EscolaListViewModel
            {
                NomesDasEscolas = EscolasId
            };


            return View(EscolasListViewModel);


        }
    }
}
