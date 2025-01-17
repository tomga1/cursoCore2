using Azure.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cursoCore2API.Models
{
    public class Producto : EntityBase
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int idProducto { get; set; }

        //[Required]
        //public string? nombre { get; set; }

        [Required]
        public int stock { get; set; }
        public string? descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal precio { get; set; }

        public string? RutaImagen { get; set; }
        public string? RutaLocalImagen { get; set; }


        public int idMarca { get; set; }

        [ForeignKey("idMarca")]
        public virtual Marcas? Marcas { get; set; }

        public int categoriaId { get; set; }
        [ForeignKey("categoriaId")]
        public Categoria Categoria { get; set; }

    }
}
