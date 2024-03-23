using Dapper;

namespace MariscosSanMartinAPI.Features.Ventas
{
    public class VentasDB
    {
        #region Nombres funciones

        //Funciones GET
        internal const string GetVentas = "sp_get_ventas";
        internal const string GetDetallesVenta = "sp_get_detalles_venta";

        //Funciones SET
        internal const string InsertVenta = "sp_insert_venta";
        internal const string InsertDetalleVenta = "sp_insert_detalle_venta";


        #endregion


        #region Parámetros

        internal static DynamicParameters GetVentasParams(FiltroVentas filtros)
        {
            DynamicParameters parameters = new(new
            {
                @p_id_venta = filtros.Id_Venta,
                @p_activo = filtros.Activo,
                @p_fecha_desde = filtros.Fecha_Desde,
                @p_fecha_hasta = filtros.Fecha_Hasta,
                @p_id_cliente = filtros.Id_Cliente,
                @p_total_desde = filtros.Total_Desde,
                @p_total_hasta = filtros.Total_Hasta

            });

            return parameters;
        }

        internal static DynamicParameters GetDetallesVentaParams(FiltroDetalleVenta filtros)
        {
            DynamicParameters parameters = new(new
            {
                @p_id_venta = filtros.Id_Venta,
                @p_activo = filtros.Activo
            });

            return parameters;
        }

        internal static DynamicParameters SetVentaParams(VentaDTO venta)
        {
            DynamicParameters parameters = new(new
            {
                @p_activo = venta.Activo,
                @p_id_usuario_modificacion = venta.Id_Usuario_Modificacion,
                @p_id_cliente = venta.Id_Cliente,
                @p_fecha_venta = venta.Fecha_Venta,
                @p_total = venta.Total,
                @p_codigo_venta = venta.Codigo_Venta

            });

            return parameters;
        }

        internal static DynamicParameters SetDetalleVentaParams(DetalleVentaDTO detalle)
        {
            DynamicParameters parameters = new(new
            {
                @p_activo = detalle.Activo,
                @p_id_usuario_modificacion = detalle.Id_Usuario_Modificacion,
                @p_id_venta = detalle.Id_Venta,
                @p_id_comida = detalle.Id_Comida,
                @p_descripcion = detalle.Descripcion,
                @p_precio = detalle.Precio,
                @p_cantidad = detalle.Cantidad,
                @p_aplica_desc = detalle.Aplica_Desc,
                @p_descuento = detalle.Descuento,
                @p_subtotal = detalle.Subtotal

            });

            return parameters;
        }

        #endregion
    }
}
