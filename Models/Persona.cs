using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Range(1, 120, ErrorMessage = "Edad inválida")]
        public int Edad { get; set; }

        public string Username { get; set; }
    }
}
