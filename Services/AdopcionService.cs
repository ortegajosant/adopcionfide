using DemoMVC.Models;
using DemoMVC.Repositories;


namespace DemoMVC.Services
{
    public class AdopcionService : IAdopcionService
    {
        private readonly IPersonaRepository _personaRepo;
        private readonly IMascotaRepository _mascotaRepo;
        private readonly IAdopcionRepository _adopcionRepo;

        public AdopcionService(
            IPersonaRepository personaRepo,
            IMascotaRepository mascotaRepo,
            IAdopcionRepository adopcionRepo)
        {
            _personaRepo = personaRepo;
            _mascotaRepo = mascotaRepo;
            _adopcionRepo = adopcionRepo;
        }

        public void Adoptar(int personaId, int mascotaId)
        {
            var persona = _personaRepo.ObtenerPorId(personaId);
            var mascota = _mascotaRepo.ObtenerPorId(mascotaId);

            if (persona == null || mascota == null)
                throw new Exception("Datos inválidos");

            if (mascota.Adoptada)
                throw new Exception("Mascota ya adoptada");

            _adopcionRepo.CrearAdopcion(persona, mascota);
        }

        public List<Adopcion> ObtenerTodas()
            => _adopcionRepo.ObtenerTodas();

        AdopcionViewModel IAdopcionService.ObtenerAdopcionViewModel()
        {
            return new AdopcionViewModel
            {
                Personas = _personaRepo.ObtenerTodas(),
                MascotasDisponibles = _mascotaRepo.ObtenerDisponibles()
            };
        }
    }
}
