using MariscosSanMartinAPI.Features.Configuracion;
using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MariscosSanMartinAPI.Features.Configuracion
{
    [ApiController]
    [Route("api/v1/configuracion")]
    public class ConfiguracionController : ControllerBase
    {
        private readonly ConfiguracionRepository _configuracionRepository;

        public ConfiguracionController(ConfiguracionRepository configuracionRepository)
        {
            _configuracionRepository = configuracionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Configuracion>>> GetDatosEmpresa()
        {
            var listadoConfiguracion = await _configuracionRepository.GetDatosEmpresa();

            if (listadoConfiguracion == null || listadoConfiguracion.Count == 0)
            {
                return NotFound();
            }

            return listadoConfiguracion;
        }

        [HttpPut("actualizar")]
        public async Task<ActionResult<Ejecucion>> ActualizarDatosEmpresa(Configuracion configuracion)
        {
            var resultado = await _configuracionRepository.ActualizarConfiguracion(configuracion);

            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }
    }
}
