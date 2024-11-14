using System.ComponentModel.DataAnnotations;

namespace cursoCoreMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public bool Admin { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        [Url]
        public string? UrlImagen { get; set; }
        public DateTime? Fecha_Nacimiento { get; set; }
    }
}
