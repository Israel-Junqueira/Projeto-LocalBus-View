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

        public IActionResult Index(string NomeEscola)
        {

            IEnumerable<Escola> EscolasIndex;

            string EscolasAtuais = string.Empty;

            if (string.IsNullOrEmpty(NomeEscola))
            {
                EscolasIndex = _context.EscolaRepository.OrderBy(E => E.EscolaId);

            }
            else
            {
                if (string.Equals("ETEC Comendador João Rayz", NomeEscola, StringComparison.OrdinalIgnoreCase))
                {
                    EscolasIndex = _context.EscolaRepository.Where(E => E.NomeEscola.Equals("ETEC Comendador João Rayz")).OrderBy(e => e.NomeEscola);
                }
                else
                {
                    EscolasIndex = _context.EscolaRepository.Where(E => E.NomeEscola.Equals("ETEC  Engenheiro Herval Bellusci")).OrderBy(e=>e.NomeEscola);
                }
            }

            var EscolasListViewModel = new EscolaListViewModel
            {
                NomesDasEscolas = EscolasIndex
            };
           

            return View(EscolasListViewModel);
        }


    }
}
