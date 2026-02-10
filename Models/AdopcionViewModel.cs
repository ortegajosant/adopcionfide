using System.ComponentModel.DataAnnotations;


namespace DemoMVC.Models
{
    public class AdopcionViewModel
    {
        [Required]
        public int PersonaId { get; set; }

        [Required]
        public int MascotaId { get; set; }

        public List<Persona> Personas { get; set; } = new();
        public List<Mascota> MascotasDisponibles { get; set; } = new();
    }
}
