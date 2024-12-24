using cursoCore2API.DTOs;
using cursoCore2API.Models;

namespace cursoCore2API.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<AppUser> GetUsuarios();
        AppUser GetUsuario(string usuarioId);
        bool IsUniqueUser(string usuario);

        Task<UsuarioLoginrespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto);



    }
}
