using Dapper;

namespace MariscosSanMartinAPI.Features.Usuarios
{
    public class UsuariosDB
    {
        #region Nombres funciones

        //Funciones GET
        internal const string GetUsuarios = "sp_get_usuarios";

        //Funciones SET
        internal const string SetPermisoUsuario = "sp_set_permiso_usuario";


        #endregion


        #region Parámetros

        internal static DynamicParameters GetUsuariosParams(FiltroUsuario filtros)
        {
            DynamicParameters parameters = new(new
            {
                @p_id_usuario = filtros.Id_Usuario,
                @p_activo = filtros.Activo,
                @p_nombre = filtros.Nombre,
                @p_correo = filtros.Correo,
                @p_cuenta_usuario = filtros.Cuenta_Usuario
            });

            return parameters;
        }

        internal static DynamicParameters SetPermisoUsuarioParams(PermisoUsuario permiso_usuario)
        {
            DynamicParameters parameters = new(new
            {
                @p_id_permiso_usuario = permiso_usuario.Id_Permiso_Usuario,
                @p_activo = permiso_usuario.Activo,
                @p_id_permiso = permiso_usuario.Id_Permiso,
                @p_id_usuario = permiso_usuario.Id_Usuario,
                @p_id_usuario_modificacion = permiso_usuario.Id_Usuario_Modificacion
            });

            return parameters;
        }

        #endregion
    }
}
