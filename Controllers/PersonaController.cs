using DemoMVC.Models;
using DemoMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    [Route("persona")]
    public class PersonaController : Controller
    {
        private readonly IPersonaService _personaService;

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var personas = _personaService.ObtenerTodas();
            return View(personas);
        }

        [HttpGet("crear")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost("crear")]
        public IActionResult Crear(Persona persona)
        {
            if (!ModelState.IsValid)
                return View(persona);

            if (!_personaService.CrearPersona(persona))
            {
                ModelState.AddModelError("Cedula", "Ya existe una persona con esta cédula");
                return View(persona);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("detalle/{id}")]
        public IActionResult Detalle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Cédula no válida");
            }

            var persona = _personaService.ObtenerDetalle(id);

            if (persona == null)
            {
                return NotFound("Persona no encontrada");
            }

            return View(persona);
        }
    }
}
