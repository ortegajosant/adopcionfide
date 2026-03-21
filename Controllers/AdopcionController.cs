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
    private readonly UserManager<Persona> _userManager;

    public AdopcionController(
        IAdopcionService service,
        UserManager<Persona> userManager)
    {
        _service = service;
        _userManager = userManager;
    }

    [HttpGet("crear")]
    public IActionResult Crear()
    {
        if (User.IsInRole("Admin"))
            return View(_service.ObtenerAdopcionViewModel());
        else
            return View(_service.ObtenerAdopcionViewModelParaUsuario());
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
                model = _service.ObtenerAdopcionViewModelParaUsuario();

            return View(model);
        }

        _service.Adoptar(model.PersonaId, model.MascotaId);
        return RedirectToAction("Index");
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        if (User.IsInRole("Admin"))
            return View(_service.ObtenerTodas());
        else
        {
            var user = await _userManager.GetUserAsync(User);
            return View(_service.ObtenerPorUsuario(user!.Id));
        }
    }
}
