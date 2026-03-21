using DemoMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signInManager;

        public AccountController(
            UserManager<Persona> userManager,
            SignInManager<Persona> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("Login")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl ?? "/");
            }

            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            return View(model);
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Verificar si la cédula ya existe
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Cedula == model.Cedula);
            
            if (existingUser != null)
            {
                ModelState.AddModelError("Cedula", "Esta cédula ya está registrada.");
                return View(model);
            }

            var persona = new Persona
            {
                UserName = model.Email,
                Email = model.Email,
                Cedula = model.Cedula,
                Nombre = model.Nombre,
                Edad = model.Edad
            };

            var result = await _userManager.CreateAsync(persona, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(persona, "User");
                await _signInManager.SignInAsync(persona, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("AccesoDenegado")]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
