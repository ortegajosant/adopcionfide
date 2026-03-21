using DemoMVC.Models;
using DemoMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("adopcion")]
[Authorize]
public class AdopcionController : Controller
{
    private readonly IAdopcionService _service;
    private readonly IPersonaService _personaService;
    private readonly IMascotaService _mascotaService;
    private readonly UserManager<Persona> _userManager;

    public AdopcionController(
        IAdopcionService service,
        IPersonaService personaService,
        IMascotaService mascotaService,
        UserManager<Persona> userManager)
    {
        _service = service;
        _personaService = personaService;
        _mascotaService = mascotaService;
        _userManager = userManager;
    }

    [HttpGet("crear")]
    public IActionResult Crear()
    {
        if (User.IsInRole("Admin"))
        {
            var model = _service.ObtenerAdopcionViewModel();
            return View(model);
        }
        else
        {
            var model = new AdopcionViewModel
            {
                MascotasDisponibles = _mascotaService.ObtenerDisponibles()
            };
            return View(model);
        }
    }

    [HttpPost("crear")]
    public async Task<IActionResult> Crear(AdopcionViewModel model)
    {
        if (!User.IsInRole("Admin"))
        {
            var user = await _userManager.GetUserAsync(User);
            model.PersonaId = user!.Id;
        }

        if (!ModelState.IsValid)
        {
            if (User.IsInRole("Admin"))
                model = _service.ObtenerAdopcionViewModel();
            else
                model.MascotasDisponibles = _mascotaService.ObtenerDisponibles();

            return View(model);
        }

        _service.Adoptar(model.PersonaId, model.MascotaId);
        return RedirectToAction("Index");
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        if (User.IsInRole("Admin"))
        {
            var adopciones = _service.ObtenerTodas();
            return View(adopciones);
        }
        else
        {
            var user = await _userManager.GetUserAsync(User);
            var adopciones = _service.ObtenerTodas()
                .Where(a => a.PersonaId == user!.Id).ToList();
            return View(adopciones);
        }
    }
}
