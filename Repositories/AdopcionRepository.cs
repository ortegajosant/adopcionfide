using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Repositories
{
    public class AdopcionRepository : IAdopcionRepository
    {
        private readonly AppDbContext _context;

        public AdopcionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CrearAdopcion(Persona persona, Mascota mascota)
        {
            var adopcion = new Adopcion
            {
                PersonaId = persona.Id,
                MascotaId = mascota.Id,
                FechaAdopcion = DateTime.Now
            };

            _context.Adopciones.Add(adopcion);

            mascota.Adoptada = true;

            _context.SaveChanges();
        }

        public List<Adopcion> ObtenerTodas()
        {
            return _context.Adopciones
                .Include(a => a.Persona)
                .Include(a => a.Mascota)
                .OrderByDescending(a => a.FechaAdopcion)
                .ToList();
        }

        List<Adopcion> IAdopcionRepository.ObtenerPorMascota(int mascotaId)
            => _context.Adopciones
                .Include(a => a.Persona)
                .Include(a => a.Mascota)
                .Where(a => a.MascotaId == mascotaId)
                .ToList();

        List<Adopcion> IAdopcionRepository.ObtenerPorPersona(int personaId)
            => _context.Adopciones
                .Include(a => a.Persona)
                .Include(a => a.Mascota)
                .Where(a => a.PersonaId == personaId)
                .ToList();
    }
}
