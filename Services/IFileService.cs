namespace DemoMVC.Services
{
    public interface IFileService
    {
        string? GuardarImagen(IFormFile? archivo);
    }
}
