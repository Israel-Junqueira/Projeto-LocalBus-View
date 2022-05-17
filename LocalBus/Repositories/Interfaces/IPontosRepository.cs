using LocalBus.Models;

namespace LocalBus.Repositories.Interfaces
{
    public interface IPontosRepository
    {
      IEnumerable<EscolaPonto> PontosRepository { get; }
       IEnumerable<Ponto> PontosAtivos { get; }
        EscolaPonto GetPontoById(int PontoId); 
    }
}
