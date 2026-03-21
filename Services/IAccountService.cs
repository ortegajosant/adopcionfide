using DemoMVC.Models;

namespace DemoMVC.Services
{
    public interface IAccountService
    {
        Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(RegisterViewModel model);
        Task<(bool Succeeded, string? ErrorMessage)> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
    }
}
