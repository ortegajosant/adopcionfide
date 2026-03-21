using System.Diagnostics;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Home/Error/{statusCode?}")]
        public IActionResult Error(int? statusCode = null)
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = statusCode
            };

            model.Message = statusCode switch
            {
                400 => "La solicitud no es válida.",
                403 => "No tienes permiso para acceder a este recurso.",
                404 => "La página que buscas no fue encontrada.",
                500 => "Ocurrió un error interno en el servidor.",
                _ => "Ocurrió un error inesperado."
            };

            return View(model);
        }
    }
}
