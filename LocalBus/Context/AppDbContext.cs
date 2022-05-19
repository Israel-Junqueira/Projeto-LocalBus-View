using LocalBus.Models;
using Microsoft.AspNetCore.Identity; // para usaar o IdentityUser
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LocalBus.Context
{
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
    
        public DbSet<Escola> Escola { get; set; }
        public DbSet<Ponto> Pontos { get; set; }
        public DbSet<EscolaPonto> EscolasPontos { get; set; }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EscolaPonto>()
              .HasOne(e => e.Escola)
              .WithMany(ep => ep.Escola_Ponto) 
              .HasForeignKey(ei => ei.EscolaId); //isso vem da EscolaPontos


            modelBuilder.Entity<EscolaPonto>()
              .HasOne(e => e.Ponto)
              .WithMany(ep => ep.Escola_Ponto)
              .HasForeignKey(ei => ei.PontoId);


            modelBuilder.Entity<ImagemEscola>()
            .HasOne(e => e.Escola)
            .WithMany(ep => ep.ImagemEscolas)
            .HasForeignKey(ei => ei.EscolaId); //isso vem da EscolaPontos


            modelBuilder.Entity<ImagemEscola>()
              .HasOne(e => e.TipoImagem)
              .WithMany(ep => ep.ImagemEscola)
              .HasForeignKey(ei => ei.TipoImagemId);


        }
         

    }
}
