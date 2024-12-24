using AutoMapper;
using cursoCore2API.DTOs;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cursoCore2API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;
        protected RespuestaAPI _respuestaApi;
        public UsuariosController(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            this._respuestaApi = new();    

        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _repository.GetUsuarios();

            var listaUsuariosDto = new List<UsuarioDto>();

            foreach (var usuario in listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(usuario));
            }
            return Ok(listaUsuariosDto);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{usuarioId}", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuario(string usuarioId)
        {
            var itemUsuario = _repository.GetUsuario(usuarioId);

            if (itemUsuario == null)
            {
                return NotFound();
            }

            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);


            return Ok(itemUsuarioDto);
        }


        [AllowAnonymous]
        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDto usuarioRegistroDto)
        {
            bool validarNombreUsuarioUnico = _repository.IsUniqueUser(usuarioRegistroDto.Nombre);
            if(!validarNombreUsuarioUnico)
            {
                _respuestaApi.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("El nombre de usuario ya existe");
                return BadRequest(_respuestaApi);
            }

            var usuario = await _repository.Registro(usuarioRegistroDto);
            if (usuario == null)
            {
                _respuestaApi.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("Error en el regstro");
                return BadRequest(_respuestaApi);
            }

            _respuestaApi.StatusCode = System.Net.HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            return Ok(_respuestaApi); 
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var respuestaLogin = await _repository.Login(usuarioLoginDto);

            if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuestaApi.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("El nombre de usuario o password son incorrectos");
                return BadRequest(_respuestaApi);
            }

            _respuestaApi.StatusCode = System.Net.HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            _respuestaApi.Result = respuestaLogin;
            return Ok(_respuestaApi);
            
    }
}
}
