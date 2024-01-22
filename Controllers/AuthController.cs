using AutoMapper;
using CRUD_Basico.Data.Interfaces;
using CRUD_Basico.Dtos;
using CRUD_Basico.Models;
using CRUD_Basico.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Basico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repository, ITokenServices tokenServices, IMapper mapper)
        {
            _repository = repository;
            _tokenServices = tokenServices;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register (UsuarioRegisterDTO usuarioDTO){
            usuarioDTO.Correo = usuarioDTO.Correo.ToLower();

            bool usuarioExistente = await _repository.ExisteUsuario(usuarioDTO.Correo); 
        
            if(usuarioExistente){
                return BadRequest("Correo no disponible");
            }

            var usuarioNuevo = _mapper.Map<Usuario>(usuarioDTO);
            var usuarioCreado = await _repository.Registrar(usuarioNuevo, usuarioDTO.Contraseña);
            var usuarioCreadoDTO = _mapper.Map<UsuariosListDTO>(usuarioCreado);
        
            return Ok(usuarioCreadoDTO);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login (UsuarioLoginDTO usuarioLoginDTO){
            var usuarioFromRepo = await _repository.Login(usuarioLoginDTO.Correo, usuarioLoginDTO.Contraseña);

            if(usuarioFromRepo == null){
                return Unauthorized();
            } 

            var usuario = _mapper.Map<UsuariosListDTO>(usuarioFromRepo);
            var token = _tokenServices.CreateToken(usuarioFromRepo);

            return Ok(new { token, usuario });
        }
    }
}