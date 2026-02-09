using DemoMVC.Models;

namespace DemoMVC.Services
{
    public interface IMascotaService
    {
        List<Mascota> ObtenerDisponibles();
        Mascota? ObtenerDetalle(int id);
        bool CrearMascota(Mascota mascota);
    }
}
