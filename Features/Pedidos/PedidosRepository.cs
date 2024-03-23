using AutoMapper;
using MariscosSanMartinAPI.Handlers;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MariscosSanMartinAPI.Features.Pedidos
{
    public class PedidosRepository
    {
        private readonly AppDBContext _contexto;
        private readonly IMapper _mapper;

        public PedidosRepository(AppDBContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        internal static List<PedidoDAO> GetPedidos(FiltroPedidos filtros)
        {

            var spParams = PedidosDB.GetPedidosParams(filtros);

            try
            {
                var ventas = DapperHandler.GetFromProcedure<PedidoDAO>(PedidosDB.GetPedidos, spParams);

                return ventas;
            }
            catch (Exception ex)
            {
                string parametros = DapperHandler.GetValuesFromInputParameters(spParams);
                //ExceptionHandler.InsertLog(ExceptionHandler.ConvertToLog(ex, null, parametros));

                throw;
            }
        }
        internal async Task<Ejecucion> SetPedido(PedidoDTO pedido)
        {
            var ejecucion = new Ejecucion() {};
            var spParams = PedidosDB.SetPedidoParams(pedido);

            try
            {
                // Utilizando Task.Run para ejecutar de manera asincrónica el código sincrónico
                ejecucion = await Task.Run(() => DapperHandler.SetFromProcedure(PedidosDB.InsertPedido, spParams));

                return ejecucion;
            }
            catch (Exception ex)
            {
                string parametros = DapperHandler.GetValuesFromInputParameters(spParams);
                // Aquí deberías insertar el log de la excepción
                // No es necesario re-lanzar la excepción si ya estás manejando el error devolviendo un objeto Ejecucion
                return new Ejecucion
                {
                    Exitoso = false,
                    Mensaje = ex.Message,
                };
            }
        }

    }
}
