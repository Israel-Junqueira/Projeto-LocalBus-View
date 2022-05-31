using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LocalBus.Repositories
{
    public class PontosRepository : IPontosRepository
    {
        private readonly AppDbContext _context;

        public PontosRepository(AppDbContext contect)
        {
            _context = contect;
        }

        public IEnumerable<Ponto> PontosAtivos => _context.Pontos.Where(p => p.AtivoPonto);

        IEnumerable<EscolaPonto> IPontosRepository.PontosDeEscolas =>_context.EscolasPontos.Include(x=>x.Ponto) ;

        public IEnumerable<Ponto> GetAllPonto() {

            return _context.Pontos;
          
        }

       IEnumerable<Escola> IPontosRepository.GetAllEscola()
        {

            return _context.Escola;

        }




        //.Select(x => new {Id_doPonto= x.PontoId,Id_daEscola= x.EscolaId,Objeto_Ponto= x.Ponto })
        // .Where(x => x.Id_daEscola.Equals(EscolaId));


        //_context.EscolasPontos
        //.Select(p => new { ID_da_escola = p.EscolaId, ID_do_Ponto = p.PontoId, Varios_pontos = p.Ponto })
        //.Include(p=>p.ID_da_escola.Equals(PontoId));

    }
    }
