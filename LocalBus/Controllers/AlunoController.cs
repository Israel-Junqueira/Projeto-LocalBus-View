﻿using LocalBus.Models;
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
            var EscolasListViewModel = new EscolaListViewModel();
            if (IdEscola == 0)
            {
                EscolasId = _context.EscolaRepository.OrderBy(E => E.EscolaId);


                EscolasListViewModel.NomesDasEscolas = EscolasId;
                
            }
           

           


            return View(EscolasListViewModel);


        }
    }
}
