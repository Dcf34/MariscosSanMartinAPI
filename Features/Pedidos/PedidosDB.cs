using Dapper;

namespace MariscosSanMartinAPI.Features.Pedidos
{
    public class PedidosDB
    {
        #region Nombres funciones

        //Funciones GET
        internal const string GetPedidos = "sp_get_pedidos";

        //Funciones SET
        internal const string InsertPedido = "sp_insert_pedido";

        #endregion


        #region Parámetros

        internal static DynamicParameters GetPedidosParams(FiltroPedidos filtros)
        {
            DynamicParameters parameters = new(new
            {
                @p_id_pedido = filtros.Id_Pedido,
                @p_activo = filtros.Activo,
                @p_id_cliente = filtros.Id_Cliente,
                @p_total_desde = filtros.Total_Desde,
                @p_total_hasta = filtros.Total_Hasta
            });

            return parameters;
        }

        internal static DynamicParameters SetPedidoParams(PedidoDTO pedido)
        {
            DynamicParameters parameters = new(new
            {
                @p_activo = pedido.Activo,
                @p_id_usuario_modificacion = pedido.Id_Usuario_Modificacion,
                @p_id_cliente = pedido.Id_Cliente,
                @p_id_venta = pedido.Id_Venta
            });

            return parameters;
        }

        #endregion
    }
}
