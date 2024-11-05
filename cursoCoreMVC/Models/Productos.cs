using System.ComponentModel.DataAnnotations;

namespace cursoCoreMVC.Models
{
    public class Productos
    {
        [Key]
        public int idProducto { get; set; }

        [Required]
        public string? nombre { get; set; }

        [Required]
        public int stock { get; set; }
        public string? descripcion { get; set; }
        public decimal precio { get; set; }
        public string? imagen { get; set; }

    }
}
