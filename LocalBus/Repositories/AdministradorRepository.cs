using LocalBus.Context;
using LocalBus.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalBus.Repositories.Interfaces
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly AppDbContext _context;

        public AdministradorRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Administrador> Admin => _context.Administrador.Include(a=>a.Escola);
    }
}
