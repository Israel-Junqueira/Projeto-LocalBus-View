using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalBus.Repositories
{
    public class PontosRepository: IPontosRepository
    {
        private readonly AppDbContext _context;

        public PontosRepository(AppDbContext contect)
        {
            _context = contect;
        }

        public IEnumerable<Ponto> PontosAtivos => _context.Ponto.Where(p => p.AtivoPonto).Include(p => p.Escola_Ponto);

        IEnumerable<EscolaPonto> IPontosRepository.PontosRepository => (IEnumerable<EscolaPonto>)_context.EscolasPontos.Select(c=> c.EscolaPontoId);

        public EscolaPonto GetPontoById(int PontosId) => _context.EscolasPontos.FirstOrDefault(p => p.PontoId == PontosId);
    }
}
