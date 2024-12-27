using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cursoCoreMVC.Models
{
    public class Marcas
    {
       
        public int idMarca { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? nombre { get; set; }
    }
}
