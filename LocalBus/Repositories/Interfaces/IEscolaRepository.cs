using LocalBus.Models;

namespace LocalBus.Repositories.Interfaces
{
    public interface IEscolaRepository
    {
        IEnumerable<Escola> EscolaRepository { get; }
        IEnumerable<Escola> NomesDasEscolas { get; }
        Escola GetPontoById(int EscolaId);
    }
}
