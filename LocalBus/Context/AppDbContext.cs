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
        public DbSet<Ponto> Pontos { get; set; }
        public DbSet<EscolaPonto> EscolasPontos { get; set; }
        public DbSet<Administrador> Administrador { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EscolaPonto>()
              .HasOne(e => e.Escola)
              .WithMany(ep => ep.Escola_Ponto) 
              .HasForeignKey(ei => ei.EscolaId); //isso vem da EscolaPontos


            modelBuilder.Entity<EscolaPonto>()
              .HasOne(e => e.Ponto)
              .WithMany(ep => ep.Escolas_Ponto)
              .HasForeignKey(ei => ei.PontoId);
        }
    }
}
