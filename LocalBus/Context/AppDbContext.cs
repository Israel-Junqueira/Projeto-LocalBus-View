using LocalBus.Models;
using Microsoft.EntityFrameworkCore;
namespace LocalBus.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Pontos> Pontos { get; set; }
        public DbSet<Administrador> Administrador { get; set; }

    }
}
