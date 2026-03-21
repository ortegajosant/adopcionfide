using DemoMVC.Constants;
using DemoMVC.Exceptions;
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
    public async Task<IActionResult> Crear()
    {
        if (User.IsInRole(Roles.Admin))
        {
            ViewBag.ModoAdmin = true;
            return View(_service.ObtenerAdopcionViewModel());
        }
        else
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.ModoAdmin = false;
            ViewBag.NombreUsuario = user!.Nombre;
            return View(_service.ObtenerAdopcionViewModelParaUsuario());
        }
    }

    [HttpPost("crear")]
    public async Task<IActionResult> Crear(AdopcionViewModel model)
    {
        if (!User.IsInRole(Roles.Admin))
        {
            var user = await _userManager.GetUserAsync(User);
            model.PersonaId = user!.Id;
        }

        if (!ModelState.IsValid)
        {
            if (User.IsInRole(Roles.Admin))
                model = _service.ObtenerAdopcionViewModel();
            else
                model = _service.ObtenerAdopcionViewModelParaUsuario();

            return View(model);
        }

        try
        {
            _service.Adoptar(model.PersonaId, model.MascotaId);
            TempData["SuccessMessage"] = "¡Adopción realizada exitosamente!";
            return RedirectToAction("Index");
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (BusinessException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Crear");
        }
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        if (User.IsInRole(Roles.Admin))
            return View(_service.ObtenerTodas());
        else
        {
            var user = await _userManager.GetUserAsync(User);
            return View(_service.ObtenerPorUsuario(user!.Id));
        }
    }
}
