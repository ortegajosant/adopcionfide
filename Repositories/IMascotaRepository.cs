using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DemoMVC.Repositories
{
    public interface IMascotaRepository
    {
        List<Mascota> ObtenerTodas();
        List<Mascota> ObtenerDisponibles();
        Mascota? ObtenerPorId(int id);
        bool ExisteId(int id);
        void Agregar(Mascota mascota);

        void Actualizar(Mascota mascota);

        void Eliminar(int id);
    }

}
