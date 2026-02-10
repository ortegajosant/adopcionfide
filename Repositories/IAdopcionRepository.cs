using DemoMVC.Models;

namespace DemoMVC.Repositories
{
    public interface IAdopcionRepository
    {
        void CrearAdopcion(Persona persona, Mascota mascota);

        List<Adopcion> ObtenerTodas();

        List<Adopcion> ObtenerPorPersona(int personaId);

        List<Adopcion> ObtenerPorMascota(int mascotaId);
    }
}
