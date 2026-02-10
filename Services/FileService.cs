using DemoMVC.Services;


namespace DemoMVC.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string? GuardarImagen(IFormFile? archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return null;

            var extension = Path.GetExtension(archivo.FileName);
            var nombreArchivo = Guid.NewGuid() + extension;

            var rutaCarpeta = Path.Combine(_env.WebRootPath, "images", "mascotas");
            Directory.CreateDirectory(rutaCarpeta);

            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            using var stream = new FileStream(rutaCompleta, FileMode.Create);
            archivo.CopyTo(stream);

            return "/images/mascotas/" + nombreArchivo;
        }
    }
}
