using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalBus.Repositories
{
    public class EscolaPontoRepository : IEscolaPontoRepository
    {
        private readonly AppDbContext _context;
        public EscolaPontoRepository(AppDbContext context )
        {
            _context = context;
        }

        public IEnumerable<EscolaPonto> EscolaPontos => _context.EscolasPontos.Include(c => c.Escola).Include(c => c.Ponto);
    }
}
