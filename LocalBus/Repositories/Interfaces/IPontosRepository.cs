using LocalBus.Models;

namespace LocalBus.Repositories.Interfaces
{
    public interface IPontosRepository
    {
        IEnumerable<Ponto> PontosRepository { get; }
        IEnumerable<Ponto> PontosAtivos { get; }
        Ponto GetPontoById(int PontoId); 
    }
}
