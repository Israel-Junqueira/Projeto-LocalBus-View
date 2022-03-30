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

        public IEnumerable<Escola> Escolas => _context.Escolas.Include(e=>e.Escola_Ponto);
    }
}
