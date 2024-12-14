using cursoCore2API.Data;
using cursoCore2API.DTOs;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace cursoCore2API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string SecretForKey; 
        private readonly StoreContext _context;
        
            

        public UsuarioRepository(StoreContext context,IConfiguration config)
         {
            _context = context;
            SecretForKey = config.GetValue<string>("Authentication:SecretForKey");
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

        public async Task<UsuarioLoginrespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptado = obtenermd5(usuarioLoginDto.Password);
            var usuario = _context.users.FirstOrDefault(u => u.Username.ToLower() == usuarioLoginDto.Username.ToLower()
                && u.Password == passwordEncriptado);

            // Validamos si el usuario no existe con la combinacion deusuario y contraseña correcta
            if(usuario == null)
            {
                return new UsuarioLoginrespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };

            }
                //Aqui existe el usuarion entonces podemos procesar el login

                var manejadoToken = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretForKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadoToken.CreateToken(tokenDescriptor);

            UsuarioLoginrespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginrespuestaDto()
            {
                Token = manejadoToken.WriteToken(token),
                Usuario = usuario
            };
            return usuarioLoginRespuestaDto;
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
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for(int i = 0; i< data.Length; i++) 
            {
                resp += data[i].ToString("x2").ToLower();
            }
            return resp;
        }
    }
}
