using cursoCore2API.Data;
using cursoCore2API.DTOs;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace cursoCore2API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
            private readonly StoreContext _context;
            

            public UsuarioRepository(StoreContext context, )
            {
                _context = context;
            }



        public User GetUsuario(int usuarioId)
        {
            return _context.users.FirstOrDefault(c => c.Id == usuarioId);
        }

        public ICollection<User> GetUsuarios()
        {
            return _context.users.OrderBy(c => c.Nombre).ToList();
        }

        public bool IsUniqueUser(string usuario)
        {
            var usuarioBd = _context.users.FirstOrDefault(u => u.Nombre == usuario);

            if(usuarioBd == null)
            {
                return true;
            }
            return false;   
        }

        public Task<UsuarioLoginrespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            var passwordEncriptado = obtenermd5(usuarioRegistroDto.Password);

            User usuario = new User()
            {
                Username = usuarioRegistroDto.Username,
                Password = passwordEncriptado,
                Nombre = usuarioRegistroDto.Nombre,
                Role = usuarioRegistroDto.Role
            };
            _context.users.Add(usuario);
            await _context.SaveChangesAsync();
            usuario.Password = passwordEncriptado;
            return usuario;
        }

        public static string obtenermd5(string valor)
        {

        }
    }
}
