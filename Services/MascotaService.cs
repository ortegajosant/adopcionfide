using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.Repositories;
using System;


namespace DemoMVC.Services
{
    public class MascotaService : IMascotaService
    {
        private readonly IMascotaRepository _repository;

        public MascotaService(IMascotaRepository repository)
        {
            _repository = repository;
        }

        public List<Mascota> ObtenerDisponibles()
            => _repository.ObtenerDisponibles();

        public Mascota? ObtenerDetalle(int id)
            => _repository.ObtenerPorId(id);

        public bool CrearMascota(Mascota mascota)
        {
            if (_repository.ExisteId(mascota.Id))
                return false;

            _repository.Agregar(mascota);
            return true;
        }
    }
}
