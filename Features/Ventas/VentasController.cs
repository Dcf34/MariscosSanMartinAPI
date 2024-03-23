using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MariscosSanMartinAPI.Features.Ventas
{
    [ApiController]
    [Route("api/v1/ventas")]
    public class VentasController : ControllerBase
    {
        private readonly VentasRepository _ventasRepository;

        public VentasController(VentasRepository ventasRepository)
        {
            _ventasRepository = ventasRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<VentaDAO>>> GetComidas([FromQuery] FiltroVentas filtros)
        {
            try
            {
                var ventas = await Task.FromResult(VentasRepository.GetVentas(filtros));

                return ventas;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("detalles")]
        public async Task<ActionResult<List<DetalleVentaDAO>>> GetDetallesVenta([FromQuery] FiltroDetalleVenta filtros)
        {
            try
            {
                var detalles_venta = await Task.FromResult(VentasRepository.GetDetalleVenta(filtros));

                return detalles_venta;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }

        [HttpPost("nueva-venta")]
        public async Task<ActionResult<Ejecucion>> CrearVenta(VentaDTO venta)
        {
            try
            {
                var ejecucion = await _ventasRepository.CrearVenta(venta);

                return ejecucion;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }
    }
}
