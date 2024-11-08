﻿using cursoCore2API.Models;
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

                var securityPassword = new SymmetricSecurityKey(Encoding.ASCCI.GetBytes(_config["Authentication:SecretForKey"]));

                SigningCredentials signature= new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", userAuthenticated.Id.ToString()));
                claimsForToken.Add(new Claim("given_name", userAuthenticated.Username));
                //claimsForToken.Add(new Claim("role", userAuthenticated.rol.Tostring())); AUTENTICAR ROL DESDE TOKEN

                var jwtSecurityToken = new JwtSecurityToken(
                    _config["Authentication:Issuer"],
                    _config["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signature);

                string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

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

