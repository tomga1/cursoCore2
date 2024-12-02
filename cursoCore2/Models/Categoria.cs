using System.ComponentModel.DataAnnotations;

namespace cursoCore2API.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]  
        public string? Nombre { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
