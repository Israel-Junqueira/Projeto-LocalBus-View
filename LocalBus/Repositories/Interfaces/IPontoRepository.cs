using LocalBus.Models;

namespace LocalBus.Repositories.Interfaces
{
    public interface IPontoRepository
    {
        public IEnumerable<Ponto> Pontos { get; }
        public IEnumerable<Ponto> AtivoPonto { get; }

        public Ponto GetPontoByiD(int ProdutoId);
    }
}
