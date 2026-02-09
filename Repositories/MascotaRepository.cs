using DemoMVC.Data;
using DemoMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace DemoMVC.Repositories
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly AppDbContext _context;

        public MascotaRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Mascota> ObtenerTodas()
            => _context.Mascotas.OrderBy(m => m.Nombre).ToList();

        public List<Mascota> ObtenerDisponibles()
            => _context.Mascotas
                .Where(m => !m.Adoptada)
                .OrderBy(m => m.Nombre)
                .ToList();

        public Mascota? ObtenerPorId(int id)
            => _context.Mascotas.Find(id);

        public bool ExisteId(int id)
            => _context.Mascotas.Any(m => m.Id == id);

        public void Agregar(Mascota mascota)
        {
            _context.Mascotas.Add(mascota);
            _context.SaveChanges();
        }

        public void Actualizar(Mascota mascota)
        {
            _context.Mascotas.Update(mascota);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var mascota = ObtenerPorId(id);
            if (mascota != null)
            {
                _context.Mascotas.Remove(mascota);
                _context.SaveChanges();
            }
        }
    }
}
