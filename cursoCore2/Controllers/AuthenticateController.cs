using cursoCore2API.Models;
using cursoCore2API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cursoCore2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        private readonly UserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthenticateController(UserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }


        [HttpPost]
        public IActionResult Authenticate([FromBody] CredentialsForAuthenticateDTO credentials)
        {

            User? userAuthenticated = _userRepository.Authenticate(credentials.Username, credentials.Password);
            if (userAuthenticated is not null)
            {

                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

                SigningCredentials signature= new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", userAuthenticated.Id.ToString()));
                claimsForToken.Add(new Claim("given_name", userAuthenticated.Username));
                if (userAuthenticated.Admin)
                {
                    claimsForToken.Add(new Claim("role", "admin"));
                }
                else
                {
                    claimsForToken.Add(new Claim("role", "user"));

                }


                var jwtSecurityToken = new JwtSecurityToken(
                    _config["Authentication:Issuer"],
                    _config["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signature);

                string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return Ok(tokenToReturn);

            }
             return Unauthorized();
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var listaUsuarios = _userRepository.GetUsers();
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }

    //[HttpPost]
    //public IActionResult Authenticate([FromBody] CredentialsForAuthenticateDTO credentials)
    //{
    //    return Ok();

    //}
}

