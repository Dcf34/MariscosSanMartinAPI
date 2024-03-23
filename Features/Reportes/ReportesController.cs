using MariscosSanMartinAPI.Features.Ventas;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MariscosSanMartinAPI.Features.Reportes
{
    [ApiController]
    [Route("api/v1/reportes")]
    public class ReportesController : ControllerBase
    {
        private readonly ReportesRepository _reportesRepository;

        public ReportesController(ReportesRepository reportesRepository)
        {
            _reportesRepository = reportesRepository;
        }

        [HttpGet("ventas")]
        public async Task<ActionResult> DescargarReportVentas([FromQuery] FiltroVentas filtros)
        {
            ContenidoArchivo contenidoArchivo = await _reportesRepository.GenerarReporteVentas(filtros);

            if (!contenidoArchivo.Exitoso)
                return StatusCode(500, new Ejecucion { Exitoso = false, Mensaje = contenidoArchivo.Mensaje });

            return File(contenidoArchivo.Contenido.ToArray(), contenidoArchivo.Tipo_Contenido, contenidoArchivo.Nombre);
        }


    }
}
