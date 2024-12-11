using System.ComponentModel.DataAnnotations;

namespace cursoCore2API.DTOs
{
    public class CategoriaDto
    {
        public int categoriaId { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage ="El numero maximo de caracteres es de 100!")]
        public string? Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
