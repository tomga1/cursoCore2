using cursoCore2API.Models;
using cursoCore2API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace cursoCore2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        private readonly UserRepository _userRepository;

        public AuthenticateController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        public IActionResult Authenticate([FromBody] CredentialsForAuthenticateDTO credentials)
        {

            User? userAuthenticated = _userRepository.Authenticate(credentials.Username, credentials.Password);
            if (userAuthenticated is not null)
            {

                //var securityPassword = new SymmetricSecurityKey(HeaderEncodingSelector.ASCCI.GetBytes(_config["Authentication:SecretForKey"]));

                //SigningCredentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                //var claimsForToken = new List<Claim>();
                //claimsForToken.Add(new Claim("sub", User.Ident.ToString()));
                //claimsForToken.Add(new Claim("given_name", User.Email));
                //claimsForToken.Add(new Claim("role", User.ro.Tostring()));

                //var jwtSecurityToken = new JwtSecurityToken(
                //    _config["Authentication:Issuer"],
                //    _config["Authentication:Audience"],
                //    claimsForToken,
                //    DateTime.UtcNow,
                //    DateTime.UtcNow.AddHours(1))




                return Ok("llaveToken");

            }
            return Unauthorized();
        }
    }

    //[HttpPost]
    //public IActionResult Authenticate([FromBody] CredentialsForAuthenticateDTO credentials)
    //{
    //    return Ok();

    //}
}

