using DemoMVC.Constants;
using DemoMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signInManager;

        public AccountService(
            UserManager<Persona> userManager,
            SignInManager<Persona> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(RegisterViewModel model)
        {
            var existingUser = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Cedula == model.Cedula);

            if (existingUser != null)
                return (false, ["Esta cédula ya está registrada."]);

            var persona = new Persona
            {
                UserName = model.Email,
                Email = model.Email,
                Cedula = model.Cedula,
                Nombre = model.Nombre,
                Edad = model.Edad
            };

            var result = await _userManager.CreateAsync(persona, model.Password);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            await _userManager.AddToRoleAsync(persona, Roles.User);
            await _signInManager.SignInAsync(persona, isPersistent: false);

            return (true, []);
        }

        public async Task<(bool Succeeded, string? ErrorMessage)> LoginAsync(string email, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(
                email, password, rememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
                return (true, null);

            return (false, "Correo o contraseña incorrectos.");
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
