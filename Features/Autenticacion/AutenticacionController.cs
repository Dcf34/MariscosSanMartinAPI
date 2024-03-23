using MariscosSanMartinAPI.Features.Autenticacion;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MariscosSanMartinAPI.Features.Autenticacion
{
    [EnableCors("AllowAnyOrigin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly AutenticacionRepository _autenticacionRepository;

        public AutenticacionController(AutenticacionRepository autenticacionRepository)
        {
            _autenticacionRepository = autenticacionRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Autenticacion>> Login(Data sesion)
        {
            try
            {
                var resultado = await _autenticacionRepository.Login(sesion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }
    }
}
