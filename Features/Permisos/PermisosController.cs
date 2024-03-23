using MariscosSanMartinAPI.Features.Permisos;
using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MariscosSanMartinAPI.Features.Permisos
{
    [ApiController]
    [Route("api/v1/permisos")]
    public class PermisosController : ControllerBase
    {
        private readonly PermisosRepository _permisosRepository;

        public PermisosController(PermisosRepository permisosRepository)
        {
            _permisosRepository = permisosRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Permiso>>> GetPermisos()
        {
            try
            {
                var permisos = await _permisosRepository.GetPermisos();

                return Ok(permisos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }

        
    }
}
