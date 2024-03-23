using MariscosSanMartinAPI.Features.Comidas;
using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MariscosSanMartinAPI.Features.Comidas
{
    [ApiController]
    [Route("api/v1/comidas")]
    public class ComidasController : ControllerBase
    {
        private readonly ComidasRepository _comidasRepository;

        public ComidasController(ComidasRepository comidasRepository)
        {
            _comidasRepository = comidasRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comida>>> GetComidas([FromQuery] FiltroComida filtros)
        {
            try
            {
                var comidas = await Task.FromResult(ComidasRepository.GetComidas(filtros));

                return Ok(comidas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }
        [HttpPost]

        public async Task<ActionResult<Ejecucion>> SetComida(Comida comida)
        {
            try
            {
                var ejecucion = await Task.FromResult(ComidasRepository.SetComida(comida));

                return Ok(ejecucion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }
    }
}
