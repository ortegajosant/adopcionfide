using DemoMVC.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    [Route("persona-frontend")]
    [Authorize(Roles = Roles.Admin)]
    public class PersonaFrontendController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("crear")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpGet("detalle/{id}")]
        public IActionResult Detalle(string id)
        {
            return View();
        }
    }
}
