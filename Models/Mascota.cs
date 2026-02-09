using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Mascota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Tipo { get; set; }

        public bool Adoptada { get; set; }
    }
}