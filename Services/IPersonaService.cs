using DemoMVC.Models;

namespace DemoMVC.Services
{
    public interface IPersonaService
    {
        List<Persona> ObtenerTodas();
        Persona? ObtenerDetalle(string cedula);
        bool CrearPersona(Persona persona);
    }
}