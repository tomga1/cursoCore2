using System.ComponentModel.DataAnnotations;

namespace cursoCore2API.Models
{
    public class Categoria : EntityBase
    {
        //[Key]
        //public int Id { get; set; }
        //[Required]  
        //public string? categoria_nombre { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
