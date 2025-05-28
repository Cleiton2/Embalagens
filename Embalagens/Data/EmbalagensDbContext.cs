using Embalagens.Models;
using Microsoft.EntityFrameworkCore;

namespace Embalagens.Data
{
    public class EmbalagensDbContext(DbContextOptions<EmbalagensDbContext> options) : DbContext(options)
    {
        public DbSet<CaixaModel> Caixas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaixaModel>(entity =>
            {
                entity.OwnsOne(c => c.Dimensoes, d =>
                {
                    d.Property(p => p.Altura).HasColumnName("Altura");
                    d.Property(p => p.Largura).HasColumnName("Largura");
                    d.Property(p => p.Comprimento).HasColumnName("Comprimento");
                });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}