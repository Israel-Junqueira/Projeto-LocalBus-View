using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalBus.Repositories
{
    public class EscolaRepository : IEscolaRepository
    {
        private readonly AppDbContext _context;
        public EscolaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Escola> NomesDasEscolas => _context.Escola.Include(p => p.NomeEscola);

        IEnumerable<Escola> IEscolaRepository.EscolaRepository => _context.Escola.Include(c => c.Escola_Ponto);

        public Escola GetEscolaGetEscolaById(int EscolaId) => _context.Escola.FirstOrDefault(e => e.EscolaId == EscolaId);


         string IEscolaRepository.GetEscolaById(string IdUsuarioLogado)
        {
           return _context.Escola.FirstOrDefault(e => e.MyUserId == IdUsuarioLogado).EscolaId.ToString();
        }
    }
}
