using Dapper;
using MariscosSanMartinAPI.Features.Clientes;

namespace MariscosSanMartinAPI.Features.Clientes
{
    public class ClientesDB
    {
        #region Nombres funciones

        //Funciones GET
        internal const string spGetClientes = "sp_get_clientes";

        //Funciones SET
        internal const string spSetCliente = "sp_set_cliente";

        #endregion


        #region Parámetros

        internal static DynamicParameters GetClientesParams(FiltroCliente filtro)
        {
            DynamicParameters parameters = new(new
            {
                @p_id_cliente = filtro.Id_Cliente,
                @p_activo = filtro.Activo
            });

            return parameters;
        }

        internal static DynamicParameters SetClienteParams(Cliente cliente)
        {
            DynamicParameters parameters = new(new
            {
                @p_id_cliente = cliente.Id_Cliente,
                @p_activo = cliente.Activo,
                @p_id_usuario_modificacion = cliente.Id_Usuario_Modificacion,
                @p_nombre = cliente.Nombre,
                @p_telefono = cliente.Telefono,
                @p_direccion = cliente.Direccion
            });

            return parameters;
        }

        #endregion
    }
}
