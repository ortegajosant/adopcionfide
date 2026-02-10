using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Adopcion> Adopciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Persona <-> Adopcion (1 a muchos)
            modelBuilder.Entity<Adopcion>()
                .HasOne(a => a.Persona)
                .WithMany(p => p.Adopciones)
                .HasForeignKey(a => a.PersonaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mascota <-> Adopcion (1 a muchos)
            modelBuilder.Entity<Adopcion>()
                .HasOne(a => a.Mascota)
                .WithMany(m => m.Adopciones)
                .HasForeignKey(a => a.MascotaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Regla de negocio: cédula única
            modelBuilder.Entity<Persona>()
                .HasIndex(p => p.Cedula)
                .IsUnique();
        }
    }
}
