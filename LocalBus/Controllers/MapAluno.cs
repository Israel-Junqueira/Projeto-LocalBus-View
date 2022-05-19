using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using LocalBus.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace LocalBus.Controllers
{
    public class MapAluno : Controller
    {


        private readonly IPontosRepository _context;

        public MapAluno(IPontosRepository context)
        {
            _context = context;
        }
        IEnumerable<EscolaPonto> PontosdeEscola;
        public IActionResult Index(int IdEscola)
        {

            PontosdeEscola = _context.PontosDeEscolas.Where(p => p.EscolaId.Equals(IdEscola));
            return View(PontosdeEscola);

            //.Where(p => p.Escola.EscolaId.Equals(IdEscola));
            //.Where(x => x.Ponto.PontoId == IdEscola);
        }
    }
}
