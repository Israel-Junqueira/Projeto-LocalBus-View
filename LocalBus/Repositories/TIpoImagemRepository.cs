using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;

namespace LocalBus.Repositories
{
    public class TIpoImagemRepository : ITipoImagemRepository
    {
        private readonly AppDbContext _appDbContext;
        public TIpoImagemRepository(AppDbContext imagemdaEscola)
        {
            _appDbContext = imagemdaEscola;
        }


        public IEnumerable<TipoImagem> GetImagemdaEscola => _appDbContext.Image;

        public IEnumerable<TipoImagem> ImagemdaEscola { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
