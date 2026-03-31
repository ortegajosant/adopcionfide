using DemoMVC.Constants;
using DemoMVC.Models;
using DemoMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers.Api
{
    [Route("api/personas")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class PersonaApiController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public PersonaApiController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            var personas = _personaService.ObtenerTodas()
                .Select(p => new { p.Cedula, p.Nombre, p.Edad, p.Email });

            return Ok(personas);
        }

        [HttpGet("{cedula}")]
        public IActionResult ObtenerPorCedula(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
                return BadRequest(new { message = "Cédula no válida." });

            var persona = _personaService.ObtenerDetalle(cedula);

            if (persona == null)
                return NotFound(new { message = "Persona no encontrada." });

            return Ok(new { persona.Cedula, persona.Nombre, persona.Edad, persona.Email });
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new { message = "Datos inválidos.", errors });
            }

            if (!_personaService.CrearPersona(persona))
                return Conflict(new { message = "Ya existe una persona con esta cédula." });

            return CreatedAtAction(nameof(ObtenerPorCedula),
                new { cedula = persona.Cedula },
                new { persona.Cedula, persona.Nombre, persona.Edad });
        }
    }
}
