using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using LocalBus.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LocalBus.Controllers
{
    public class MapAluno : Controller
    {


        private readonly IPontosRepository _context;

        public MapAluno(IPontosRepository context)
        {
            _context = context;
        }

        public IActionResult Index(int IdEscola)
        {


            IEnumerable<Ponto> EscolasId;

            string EscolasAtuais = string.Empty;

            if (IdEscola == 0)
            {
                EscolasId = (IEnumerable<Ponto>)_context.PontosRepository.OrderBy(E => E.PontoId);

            }
            else
            {
                EscolasId = (IEnumerable<Ponto>)_context.GetPontoById(IdEscola);
                    
            }

            var PontosDaEscolaIndexViewModel = new PontosDaEscolaIndexViewModel
            {
               PontoDaescola = EscolasId
            }; 


            return View(PontosDaEscolaIndexViewModel);

        }
    }
}
