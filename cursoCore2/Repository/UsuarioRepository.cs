using AutoMapper;
using cursoCore2API.Data;
using cursoCore2API.DTOs;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XAct;
using XSystem.Security.Cryptography;

namespace cursoCore2API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string SecretForKey; 
        private readonly StoreContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
            

        public UsuarioRepository(StoreContext context,IConfiguration config, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
         {
            _context = context;
            SecretForKey = config.GetValue<string>("Authentication:SecretForKey");
            _userManager = userManager; 
            _roleManager = roleManager; 
            _mapper = mapper;   
         }



        public AppUser GetUsuario(string usuarioId)
        {
            return _context.AppUser.FirstOrDefault(c => c.Id == usuarioId);
        }

        public ICollection<AppUser> GetUsuarios()
        {
            return _context.AppUser.OrderBy(c => c.UserName).ToList();
        }

        public bool IsUniqueUser(string usuario)
        {
            var usuarioBd = _context.AppUser.FirstOrDefault(u => u.UserName == usuario);

            if(usuarioBd == null)
            {
                return true;
            }
            return false;   
        }

        public async Task<UsuarioLoginrespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            //var passwordEncriptado = obtenermd5(usuarioLoginDto.Password);


            var usuario = _context.AppUser.FirstOrDefault(u => u.UserName.ToLower() == usuarioLoginDto.Username.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(usuario, usuarioLoginDto.Password);
                


            // Validamos si el usuario no existe con la combinacion deusuario y contraseña correcta
            if(usuario == null || isValid == false)
            {
                return new UsuarioLoginrespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };

            }
                //Aqui existe el usuarion entonces podemos procesar el login

                var roles = await _userManager.GetRolesAsync(usuario);
                var manejadoToken = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretForKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadoToken.CreateToken(tokenDescriptor);

            UsuarioLoginrespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginrespuestaDto()
            {
                Token = manejadoToken.WriteToken(token),
                Usuario = _mapper.Map<UsuarioDatosDto>(usuario),
            };
            return usuarioLoginRespuestaDto;
        }

        public async Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            //var passwordEncriptado = obtenermd5(usuarioRegistroDto.Password);

            AppUser usuario = new AppUser()
            {
                UserName = usuarioRegistroDto.Username,
                Email = usuarioRegistroDto.Username,
                NormalizedEmail = usuarioRegistroDto.Nombre.ToUpper(),
                Name = usuarioRegistroDto.Nombre
            };

            var result = await _userManager.CreateAsync(usuario, usuarioRegistroDto.Password);

            if(result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("Usuario"));
                }

                await _userManager.AddToRoleAsync(usuario, "Admin");
                var usuarioRetornado = _context.AppUser.FirstOrDefault(u => u.UserName == usuarioRegistroDto.Username);

                return _mapper.Map<UsuarioDatosDto>(usuarioRetornado);
            }

            //_context.users.Add(usuario);
            //await _context.SaveChangesAsync();
            //usuario.Password = passwordEncriptado;
            return new UsuarioDatosDto();
        }

        //public static string obtenermd5(string valor)
        //{
        //    MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
        //    byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
        //    data = x.ComputeHash(data);
        //    string resp = "";
        //    for(int i = 0; i< data.Length; i++) 
        //    {
        //        resp += data[i].ToString("x2").ToLower();
        //    }
        //    return resp;
        //}
    }
}
