using LocalBus.Models;

namespace LocalBus.Repositories.Interfaces
{
    public interface IEscolaPontoRepository
    {
        IEnumerable<EscolaPonto> EscolaPontos { get; }
    }
}
