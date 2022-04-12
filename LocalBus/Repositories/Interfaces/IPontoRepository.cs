using LocalBus.Models;

namespace LocalBus.Repositories.Interfaces
{
    public interface IPontoRepository
    {
        IEnumerable<Ponto> Pontosrepository { get; }
    }
}
