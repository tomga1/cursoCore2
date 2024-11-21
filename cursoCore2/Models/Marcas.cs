using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cursoCore2API.Models
{
    public class Marcas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMarca { get; set; }

        [Required]
        public string? nombre { get; set; }
    }
}
