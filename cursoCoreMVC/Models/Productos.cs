using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cursoCoreMVC.Models
{
    public class Productos
    {

        public int idProducto { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? nombre { get; set; }

        [Required]
        public int stock { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string? descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal precio { get; set; }

        public IFormFile imagen { get; set; } // para subir la imagen del producto
        public string? RutaImagen { get; set; }


        public int idMarca { get; set; }

        [ForeignKey("idMarca")]
        public virtual Marcas? Marcas { get; set; }

        public int categoriaId { get; set; }
        [ForeignKey("categoriaId")]
        public Categoria Categoria { get; set; }

    }
}
