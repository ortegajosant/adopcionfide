namespace DemoMVC.Models
{
    public class MascotaViewModel
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }

        // Solo para el formulario
        public IFormFile? Imagen { get; set; }
    }
}
