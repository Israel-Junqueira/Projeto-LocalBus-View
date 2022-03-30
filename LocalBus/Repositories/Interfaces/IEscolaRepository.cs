using LocalBus.Models;

namespace LocalBus.Repositories.Interfaces
{
    public interface IEscolaRepository
    {
        public IEnumerable<Escola> Escolas { get; }
    }
}
