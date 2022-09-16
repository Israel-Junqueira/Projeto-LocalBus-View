using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using LocalBus.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace LocalBus.Controllers
{
    [Area("Usuario")]
    public class MapAluno : Controller
    {


        private readonly IPontosRepository _context;

        public MapAluno(IPontosRepository context)
        {
            _context = context;
        }
        
        public IActionResult Index(int IdEscola)
        {

            var pontosdeEscola = _context.PontosDeEscolas.Where(p => p.EscolaId.Equals(IdEscola));

            var resultadoTolist = pontosdeEscola.ToList();
            return View(resultadoTolist);



        }
    }
}
