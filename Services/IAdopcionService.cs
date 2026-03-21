using DemoMVC.Models;


namespace DemoMVC.Services
{
    public interface IAdopcionService
    {
        void Adoptar(int personaId, int mascotaId);
        AdopcionViewModel ObtenerAdopcionViewModel();
        AdopcionViewModel ObtenerAdopcionViewModelParaUsuario();
        List<Adopcion> ObtenerTodas();
        List<Adopcion> ObtenerPorUsuario(int personaId);
    }
}
