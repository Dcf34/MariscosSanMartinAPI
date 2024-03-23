using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MariscosSanMartinAPI.Features.Usuarios
{
    [ApiController]
    [Route("api/v1/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosRepository _usuariosRepository;

        public UsuariosController(UsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> GetUsuarios([FromQuery] FiltroUsuario filtros)
        {
            try
            {
                var usuarios = await Task.FromResult(UsuariosRepository.GetUsuarios(filtros));

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("permisos")]
        public async Task<ActionResult<List<PermisoUsuario>>> GetPermisosUsuario(int id_usuario)
        {
            var permisos = await _usuariosRepository.GetPermisosUsuario(id_usuario);

            if (permisos == null)
            {
                return NotFound();
            }

            return permisos;
        }

        [HttpGet("{usuario}")]
        public async Task<ActionResult<List<Usuario>>> GetUsuariosPorCorreo(string usuario)
        {
            var usuarios = await _usuariosRepository.GetUsuariosPorCorreo(usuario);

            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound();
            }

            return usuarios;
        }

        [HttpGet("perfil-usuario")]
        public async Task<ActionResult<UsuarioPerfil>> GetPerfilUsuario(int id_usuario)
        {
            var perfilUsuario = await _usuariosRepository.GetPerfilUsuario(id_usuario);

            if (perfilUsuario == null)
            {
                return NotFound(new { Mensaje = "Usuario no encontrado." });
            }

            return Ok(perfilUsuario);
        }

        [HttpPost("crear")]
        public async Task<ActionResult<Ejecucion>> CrearUsuario([FromBody] UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var resultado = await _usuariosRepository.CrearUsuario(usuarioCreacionDTO);

            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [HttpPut("actualizar-perfil")]
        public async Task<ActionResult<Ejecucion>> ActualizarPerfilUsuario([FromBody] UsuarioPerfil perfilUsuario)
        {
            if (perfilUsuario == null)
            {
                return BadRequest(new { Mensaje = "Solicitud inválida." });
            }

            var resultado = await _usuariosRepository.ActualizarUsuarioConPerfil(perfilUsuario);

            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [HttpPut("actualizar")]
        public async Task<ActionResult<Ejecucion>> ActualizarUsuario([FromBody] UsuarioActualizacionDTO usuarioActualizacionDTO)
        {
            var resultado = await _usuariosRepository.ActualizarUsuario(usuarioActualizacionDTO);

            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [HttpPost("permisos/set")]
        public async Task<ActionResult<Ejecucion>> CrearPermisosUsuario(List<PermisoUsuario> permisos_usuarios)
        {
            var resultado = await _usuariosRepository.CrearPermisosUsuario(permisos_usuarios);

            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Ejecucion>> EliminarUsuario(int id_usuario)
        {
            var resultado = await _usuariosRepository.EliminarUsuario(id_usuario);

            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        //[HttpPost("Setpermisos")]
        //public async Task<ActionResult<Ejecucion>> SetPermisoUsuario([FromQuery] PermisoUsuario permiso_usuario)
        //{
        //    try
        //    {
        //        var ejecucion = await Task.FromResult(UsuariosRepository.SetPermisoUsuario(permiso_usuario));

        //        return Ok(ejecucion);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
        //    }
        //}
    }
}
