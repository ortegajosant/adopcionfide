using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.Repositories;
using System;

namespace DemoMVC.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _repository;

        public PersonaService(IPersonaRepository repository)
        {
            _repository = repository;
        }

        public List<Persona> ObtenerTodas()
            => _repository.ObtenerTodas();

        public Persona? ObtenerDetalle(string cedula)
            => _repository.ObtenerPorCedula(cedula);

        public bool CrearPersona(Persona persona)
        {
            if (_repository.ExisteCedula(persona.Cedula))
                return false;

            _repository.Agregar(persona);
            return true;
        }
    }
}
