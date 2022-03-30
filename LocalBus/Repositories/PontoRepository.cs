using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalBus.Repositories
{
    public class PontoRepository : IPontoRepository
    {

        private readonly AppDbContext _context;
        public IEnumerable<Ponto> Pontos => _context.Pontos.Include(c=> c.Escolas_Ponto);

        public IEnumerable<Ponto> AtivoPonto => _context.Pontos.Where(a=> a.AtivoPonto).Include(e => e.Escolas_Ponto);

        public Ponto GetPontoByiD(int ProdutoId)
        {
            throw new NotImplementedException();
        }
    }
}
