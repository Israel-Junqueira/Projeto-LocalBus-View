using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalBus.Repositories
{
    public class PontosRepository: IPontosRepository
    {
        private readonly AppDbContext _contect;

        public PontosRepository(AppDbContext contect)
        {
            _contect = contect;
        }

        public IEnumerable<Ponto> PontosAtivos => _contect.Ponto.Where(p => p.AtivoPonto).Include(p => p.Escola_Ponto);

        IEnumerable<Ponto> IPontosRepository.PontosRepository => _contect.Ponto.Include(c=> c.Escola_Ponto);

        public Ponto GetPontoById(int PontosId) => _contect.Ponto.FirstOrDefault(p => p.PontoId == PontosId);
    }
}
