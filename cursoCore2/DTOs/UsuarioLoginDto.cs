using System.ComponentModel.DataAnnotations;

namespace cursoCore2API.DTOs
{
    public class UsuarioLoginDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
