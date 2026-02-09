using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace DemoMVC.Repositories;

public class PersonaRepository : IPersonaRepository
{
    private readonly AppDbContext _context;

    public PersonaRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Persona> ObtenerTodas()
        => _context.Personas.OrderBy(p => p.Nombre).ToList();

    public Persona? ObtenerPorCedula(string cedula)
        => _context.Personas.FirstOrDefault(p => p.Cedula == cedula);

    public bool ExisteCedula(string cedula)
        => _context.Personas.Any(p => p.Cedula == cedula);

    public void Agregar(Persona persona)
    {
        _context.Personas.Add(persona);
        _context.SaveChanges();
    }

    public void Actualizar(Persona persona)
    {
        _context.Personas.Update(persona);
        _context.SaveChanges();
    }

    public void Eliminar(string cedula)
    {
        var persona = ObtenerPorCedula(cedula);
        if (persona != null)
        {
            _context.Personas.Remove(persona);
            _context.SaveChanges();
        }
    }
}
