using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aviso> Avisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações da entidade Aviso (se quiser deixar mais bonitinho)
            modelBuilder.Entity<Aviso>(entity =>
            {
                entity.ToTable("Avisos");

                entity.HasKey(a => a.Id);

                entity.Property(a => a.Titulo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.Mensagem)
                    .IsRequired()
                    .HasMaxLength(500);
            });
        }
    }

}
