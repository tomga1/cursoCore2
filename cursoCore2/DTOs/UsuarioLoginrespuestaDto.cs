namespace cursoCore2API.DTOs
{
    public class UsuarioLoginrespuestaDto
    {
        public UsuarioDatosDto Usuario { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
