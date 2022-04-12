using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalBus.Repositories
{
    public class PontoRepository : IPontoRepository
    {
        private readonly AppDbContext _context;
        public PontoRepository(AppDbContext context )
        {
            _context = context;
        }

        public IEnumerable<Ponto> Pontosrepository => _context.Pontos;
    }
}
