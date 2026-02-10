using DemoMVC.Models;
using DemoMVC.Services;
using Microsoft.AspNetCore.Mvc;

[Route("adopcion")]
public class AdopcionController : Controller
{
    private readonly IAdopcionService _service;
    private readonly IPersonaService _personaService;
    private readonly IMascotaService _mascotaService;

    public AdopcionController(
        IAdopcionService service,
        IPersonaService personaService,
        IMascotaService mascotaService)
    {
        _service = service;
        _personaService = personaService;
        _mascotaService = mascotaService;
    }

    [HttpGet("crear")]
    public IActionResult Crear()
    {
        var model = _service.ObtenerAdopcionViewModel();
        return View(model);
    }

    [HttpPost("crear")]
    public IActionResult Crear(AdopcionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model = _service.ObtenerAdopcionViewModel();
            return View(model);
        }

        _service.Adoptar(model.PersonaId, model.MascotaId);
        return RedirectToAction("Index");
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        var adopciones = _service.ObtenerTodas();
        return View(adopciones);
    }
}
