using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cursoCore2API.DTOs
{
    public class UsuarioDto
    {
        
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        public bool Admin { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string? UrlImagen { get; set; }
        public string Role { get; set; }
        public DateTime? Fecha_Nacimiento { get; set; }

        
    }
}
