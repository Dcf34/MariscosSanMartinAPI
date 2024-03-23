using MariscosSanMartinAPI.Features.Clientes;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MariscosSanMartinAPI.Features.Clientes
{
    [ApiController]
    [Route("api/v1/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesRepository _clientesRepository;

        public ClientesController(ClientesRepository clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetClientes([FromQuery] FiltroCliente filtros)
        {
            try
            {
                var clientes = await Task.FromResult(ClientesRepository.GetClientes(filtros));

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Ejecucion>> SetCliente(Cliente cliente)
        {
            try
            {
                var ejecucion = await Task.FromResult(ClientesRepository.SetCliente(cliente));

                return Ok(ejecucion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = ex.Message });
            }
        }
    }
}
