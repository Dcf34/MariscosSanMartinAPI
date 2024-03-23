using AutoMapper;
using MariscosSanMartinAPI.Features.Pedidos;
using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Features.Ventas;
using MariscosSanMartinAPI.Handlers;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MariscosSanMartinAPI.Features.Ventas
{
    public class VentasRepository
    {
        private readonly AppDBContext _contexto;
        private readonly PedidosRepository _pedidosRepository;
        private readonly IMapper _mapper;

        public VentasRepository(AppDBContext contexto, IMapper mapper, PedidosRepository pedidosRepository)
        {
            _contexto = contexto;
            _mapper = mapper;
            _pedidosRepository = pedidosRepository;
        }

        internal static List<VentaDAO> GetVentas(FiltroVentas filtros)
        {

            var spParams = VentasDB.GetVentasParams(filtros);

            try
            {
                var ventas = DapperHandler.GetFromProcedure<VentaDAO>(VentasDB.GetVentas, spParams);

                return ventas;
            }
            catch (Exception ex)
            {
                string parametros = DapperHandler.GetValuesFromInputParameters(spParams);
                //ExceptionHandler.InsertLog(ExceptionHandler.ConvertToLog(ex, null, parametros));

                throw;
            }
        }

        internal static List<DetalleVentaDAO> GetDetalleVenta(FiltroDetalleVenta filtros)
        {

            var spParams = VentasDB.GetDetallesVentaParams(filtros);

            try
            {
                var detalles_venta = DapperHandler.GetFromProcedure<DetalleVentaDAO>(VentasDB.GetDetallesVenta, spParams);

                return detalles_venta;
            }
            catch (Exception ex)
            {
                string parametros = DapperHandler.GetValuesFromInputParameters(spParams);
                //ExceptionHandler.InsertLog(ExceptionHandler.ConvertToLog(ex, null, parametros));

                throw;
            }
        }

        internal async Task<Ejecucion> SetVenta(VentaDTO ventaDTO)
        {
            // Genera y asigna el código de venta único antes de definir los parámetros para el procedimiento almacenado.
            ventaDTO.Codigo_Venta = await GenerarYVerificarCodigoVentaAsync();

            var spParams = VentasDB.SetVentaParams(ventaDTO);

            try
            {
                // Aquí estamos simulando una operación asincrónica con Task.Run
                // Nota: Si DapperHandler.SetFromProcedure es en realidad asincrónico y soporta async, deberías llamarlo directamente sin Task.Run
                var ejecucion = await Task.Run(() => DapperHandler.SetFromProcedure(VentasDB.InsertVenta, spParams));

                return ejecucion;
            }
            catch (Exception ex)
            {
                string parametros = DapperHandler.GetValuesFromInputParameters(spParams);
                // Aquí insertarías el log de la excepción.
                return new Ejecucion
                {
                    Exitoso = false,
                    Mensaje = ex.Message,
                };
            }
        }

        internal static async Task<Ejecucion> SetDetalleVenta(DetalleVentaDTO detalleVenta)
        {
            var spParams = VentasDB.SetDetalleVentaParams(detalleVenta);

            try
            {
                // Utilizando Task.Run para ejecutar de manera asincrónica el código sincrónico
                var ejecucion = await Task.Run(() => DapperHandler.SetFromProcedure(VentasDB.InsertDetalleVenta, spParams));

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

        public static string GenerarCodigoVenta(int longitud)
        {
            var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var codigo = new char[longitud];
            var random = new Random();

            for (int i = 0; i < codigo.Length; i++)
            {
                codigo[i] = caracteres[random.Next(caracteres.Length)];
            }

            return new String(codigo);
        }

        public async Task<string> GenerarYVerificarCodigoVentaAsync()
        {
            string codigoVenta;
            bool existeCodigo;

            do
            {
                codigoVenta = GenerarCodigoVenta(10); // Asume que ya tienes este método
                existeCodigo = await ExisteCodigoVentaAsync(codigoVenta);
            }
            while (existeCodigo); // Si el código ya existe, genera uno nuevo

            return codigoVenta;
        }

        public async Task<bool> ExisteCodigoVentaAsync(string codigoVenta)
        {
            return await _contexto.Ventas.AnyAsync(v => v.Codigo_Venta == codigoVenta);
        }

        public async Task<Ejecucion> CrearVenta(VentaDTO venta)
        {
            var ejecucion = new Ejecucion() { };

            try
            {
                ejecucion = await SetVenta(venta);

                var idVenta = ejecucion.Id ?? 0;

                if(ejecucion.Exitoso == true)
                {
                    var detallesVenta = venta.Detalles_Venta;

                    if (detallesVenta != null && detallesVenta.Length > 0)
                    {
                        foreach (var detalle in detallesVenta)
                        {
                            detalle.Id_Venta = idVenta;

                            await SetDetalleVenta(detalle);
                        }
                    }

                    //Crear registro de pedido
                    PedidoDTO pedido = new PedidoDTO
                    {
                        // Configura las propiedades de tu pedido aquí, por ejemplo:
                        Id_Venta = idVenta,
                        Id_Cliente = venta.Id_Cliente,
                        Id_Usuario_Modificacion = venta.Id_Usuario_Modificacion,
                        Activo = true
                    };

                    // Ahora llama a SetPedido usando la instancia de PedidosRepository
                    var resultadoPedido = await _pedidosRepository.SetPedido(pedido);

                    ejecucion = new Ejecucion()
                    {
                        Id = null,
                        Mensaje = "La venta ha sido registrada exitosamente.",
                        Exitoso = true
                    };
                }
                else
                {
                    ejecucion = new Ejecucion()
                    {
                        Id = null,
                        Mensaje = "Error al registrar la venta.",
                        Exitoso = false
                    };
                }

                return ejecucion;
            }
            catch (Exception ex)
            {
                //ExceptionHandler.InsertLog(ExceptionHandler.ConvertToLog(ex, null, parametros));

                return new()
                {
                    Exitoso = false,
                    Mensaje = ex.Message,
                };
                throw;
            }
        }

    }
}
