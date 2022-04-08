using LocalBus.Models;

namespace LocalBus.Repositories.Interfaces
{
    public interface ITipoImagemRepository
    {
        public IEnumerable<TipoImagem> ImagemdaEscola { get; set; }
    }
}
