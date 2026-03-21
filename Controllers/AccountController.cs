using DemoMVC.Models;
using DemoMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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

            var (succeeded, errorMessage) = await _accountService.LoginAsync(
                model.Email, model.Password, model.RememberMe);

            if (succeeded)
                return LocalRedirect(returnUrl ?? "/");

            ModelState.AddModelError(string.Empty, errorMessage!);
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

            var (succeeded, errors) = await _accountService.RegisterAsync(model);

            if (succeeded)
                return RedirectToAction("Index", "Home");

            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);

            return View(model);
        }

        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("AccesoDenegado")]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
