using System.ComponentModel.DataAnnotations;

namespace cursoCoreMVC.Models
{
    public class Categoria
    {
        public Categoria()
        {
            FechaCreacion = DateTime.Now;
        }

        public int categoriaId { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? categoria_nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
