using LocalBus.Models;

namespace LocalBus.Repositories.Interfaces
{
    public interface IAdministradorRepository
    {

        public IEnumerable<Administrador> Admin { get; }
    }
}
