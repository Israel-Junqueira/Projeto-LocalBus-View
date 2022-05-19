using LocalBus.Models;
using System.Collections.ObjectModel;

namespace LocalBus.Repositories.Interfaces
{
    public interface IPontosRepository
    {
      IEnumerable<EscolaPonto> PontosDeEscolas { get; }
       IEnumerable<Ponto> PontosAtivos { get; }
        public IEnumerable<Ponto> GetPontoById(int PontoId); 
    }
}
