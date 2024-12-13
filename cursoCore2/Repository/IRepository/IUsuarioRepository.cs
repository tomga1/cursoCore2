using cursoCore2API.DTOs;
using cursoCore2API.Models;

namespace cursoCore2API.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<User> GetUsuarios();
        User GetUsuario(int usuarioId);
        bool IsUniqueUser(string usuario);

        Task<UsuarioLoginrespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<User> Registro(UsuarioRegistroDto usuarioRegistroDto);



    }
}
