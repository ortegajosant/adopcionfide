using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DemoMVC.Models
{
    public class Persona : IdentityUser<int>
    {
        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Range(1, 120, ErrorMessage = "Edad inválida")]
        public int Edad { get; set; }

        public List<Adopcion> Adopciones { get; set; } = new();
    }
}
