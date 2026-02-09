using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DemoMVC.Repositories
{
    public interface IPersonaRepository
    {
        List<Persona> ObtenerTodas();
        Persona? ObtenerPorCedula(string cedula);
        bool ExisteCedula(string cedula);
        void Agregar(Persona persona);

        void Actualizar(Persona persona);

        void Eliminar(string cedula);
    }

}
