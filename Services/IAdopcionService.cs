using DemoMVC.Models;


namespace DemoMVC.Services
{
    public interface IAdopcionService
    {
        void Adoptar(int personaId, int mascotaId);
        AdopcionViewModel ObtenerAdopcionViewModel();
        List<Adopcion> ObtenerTodas();
    }
}
