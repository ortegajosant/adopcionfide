using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    public class Adopcion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FechaAdopcion { get; set; } = DateTime.Now;

        // FK hacia Persona
        [ForeignKey(nameof(Persona))]
        public int PersonaId { get; set; }
        public Persona? Persona { get; set; }

        // FK hacia Mascota
        [ForeignKey(nameof(Mascota))]
        public int MascotaId { get; set; }
        public Mascota? Mascota { get; set; }
    }
}
