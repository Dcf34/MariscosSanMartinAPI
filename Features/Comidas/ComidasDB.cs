using Dapper;

namespace MariscosSanMartinAPI.Features.Comidas
{
    public class ComidasDB
    {
        #region Nombres funciones

        //Funciones GET
        internal const string GetComidas = "sp_get_comidas";

        //Funciones SET
        internal const string SetComida = "sp_set_comida";

        #endregion


        #region Parámetros

        internal static DynamicParameters GetComidasParams(FiltroComida filtro)
        {
            DynamicParameters parameters = new(new
            {
                @p_id_comida = filtro.Id_Comida,
                @p_activo = filtro.Activo
            });

            return parameters;
        }

        internal static DynamicParameters SetComidaParams(Comida comida)
        {
            DynamicParameters parameters = new(new
            {
                @p_id_comida = comida.Id_Comida,
                @p_activo = comida.Activo,
                @p_nombre = comida.Nombre,
                @p_codigo = comida.Codigo,
                @p_precio = comida.Precio,
                @p_descripcion = comida.Descripcion,
                @p_id_usuario_modificacion = comida.Id_Usuario_Modificacion
            });

            return parameters;
        }

        #endregion
    }
}
