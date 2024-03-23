using MariscosSanMartinAPI.Features.Pedidos;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MariscosSanMartinAPI.Features.Pedidos
{
    [ApiController]
    [Route("api/v1/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosRepository _pedidosRepository;

        public PedidosController(PedidosRepository pedidosRepository)
        {
            _pedidosRepository = pedidosRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PedidoDAO>>> GetPedidos([FromQuery] FiltroPedidos filtros)
        {
            try
            {
                var pedidos = await Task.FromResult(PedidosRepository.GetPedidos(filtros));

                return pedidos;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }
    }
}
