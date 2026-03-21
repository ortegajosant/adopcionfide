using DemoMVC.Models;
using DemoMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    [Route("mascota")]
    public class MascotaController : Controller
    {
        private readonly IMascotaService _mascotaService;

        public MascotaController(IMascotaService mascotaService)
        {
            _mascotaService = mascotaService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var mascotas = _mascotaService.ObtenerDisponibles();
            return View(mascotas);
        }

        [HttpGet("detalle/{id:int}")]
        public IActionResult Detalle(int id)
        {
            var mascota = _mascotaService.ObtenerDetalle(id);

            if (mascota == null)
                return NotFound();

            return View(mascota);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("crear")]
        public IActionResult Crear()
        {
            return View(new MascotaViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("crear")]
        public IActionResult Crear(MascotaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _mascotaService.CrearDesdeMascotaViewModel(model);

            return RedirectToAction("Index");
        }
    }
}
