using System.ComponentModel.DataAnnotations;

namespace cursoCore2API.Models
{
    public class Categoria
    {
        [Key]
        public int categoriaId { get; set; }
        [Required]  
        public string? categoria_nombre { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
