using AutoMapper;
using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Handlers;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MariscosSanMartinAPI.Features.Usuarios
{
    public class UsuariosRepository
    {
        private readonly AppDBContext _contexto;
        private readonly IMapper _mapper;

        public UsuariosRepository(AppDBContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        internal static List<UsuarioDTO> GetUsuarios(FiltroUsuario filtros)
        {
            //var usuarios = await _contexto.Usuarios.OrderBy(g => g.Id_Usuario).ToListAsync();

            //// Usa AutoMapper para mapear de la lista de Usuarios a UsuarioDTO
            //var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

            //return usuariosDTO;

            var spParams = UsuariosDB.GetUsuariosParams(filtros);

            try
            {
                var usuarios = DapperHandler.GetFromProcedure<UsuarioDTO>(UsuariosDB.GetUsuarios, spParams);

                return usuarios;
            }
            catch (Exception ex)
            {
                string parametros = DapperHandler.GetValuesFromInputParameters(spParams);
                //ExceptionHandler.InsertLog(ExceptionHandler.ConvertToLog(ex, null, parametros));

                throw;
            }
        }

        internal async Task<UsuarioPerfil> GetPerfilUsuario(int idUsuario)
        {
            // Obtener el usuario de la base de datos.
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(g => g.Id_Usuario == idUsuario);

            // Verificar si el usuario existe.
            if (usuario != null)
            {
                // Decodificar la clave si existe.
                usuario.Clave = StringHandler.Base64Decode(usuario.Clave ?? "");
            }

            // Mapear el usuario a UsuarioPerfil (ya con la clave decodificada).
            UsuarioPerfil usuarioPerfil = _mapper.Map<UsuarioPerfil>(usuario);

            return usuarioPerfil;
        }

        internal async Task<Ejecucion> ActualizarUsuarioConPerfil(UsuarioPerfil perfilUsuario)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id_Usuario == perfilUsuario.Id_Usuario);

            if (usuario != null)
            {
                usuario.Nombre = perfilUsuario.Nombre;
                usuario.Correo = perfilUsuario.Correo;
                usuario.Cuenta_Usuario = perfilUsuario.Cuenta_Usuario;
                usuario.Telefono = perfilUsuario.Telefono;

                if (!string.IsNullOrWhiteSpace(perfilUsuario.Clave))
                {
                    // Actualiza la clave en base64 solo si se proporciona una nueva.
                    usuario.Clave = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(perfilUsuario.Clave));
                }

                usuario.Id_Usuario_Modificacion = perfilUsuario.Id_Usuario_Modificacion;
                usuario.Fecha_Modificacion = DateTime.Now;

                _contexto.Usuarios.Update(usuario);

                await _contexto.SaveChangesAsync();

                // Devuelve un objeto Ejecucion indicando que la actualización fue exitosa.
                return new Ejecucion
                {
                    Exitoso = true,
                    Mensaje = "Los datos del usuario han sido actualizados exitosamente.",
                    Id = null // Siempre null según el ejemplo proporcionado.
                };
            }
            else
            {
                // Devuelve un objeto Ejecucion indicando que ocurrió un error.
                return new Ejecucion
                {
                    Exitoso = false,
                    Mensaje = "Error al actualizar los datos del usuario.",
                    Id = null // Siempre null según el ejemplo proporcionado.
                };
            }
        }

        internal static Ejecucion SetPermisoUsuario(PermisoUsuario permiso_usuario)
        {
            
            var spParams = UsuariosDB.SetPermisoUsuarioParams(permiso_usuario);

            try
            {
                var ejecucion = DapperHandler.SetFromProcedure(UsuariosDB.SetPermisoUsuario, spParams);

                return ejecucion;
            }
            catch (Exception ex)
            {
                string parametros = DapperHandler.GetValuesFromInputParameters(spParams);
                //ExceptionHandler.InsertLog(ExceptionHandler.ConvertToLog(ex, null, parametros));

                return new()
                {
                    Exitoso = false,
                    Mensaje = ex.Message,
                };
                throw;
            }
        }

        public async Task<List<Usuario>> GetUsuariosPorCorreoClave(string usuario, string clave)
        {
            return await _contexto.Usuarios.Where(u => u.Cuenta_Usuario == usuario && u.Clave == clave).ToListAsync();
        }

        public async Task<List<PermisoUsuario>> GetPermisosUsuario(int id_usuario)
        {
            return await _contexto.Permisos_Usuarios.Where(u => u.Id_Usuario == id_usuario).ToListAsync();
        }

        public async Task<List<Usuario>> GetUsuariosPorCorreo(string usuario)
        {
            return await _contexto.Usuarios.Where(u => u.Cuenta_Usuario == usuario).ToListAsync();
        }

        public async Task<Ejecucion> CrearUsuario(UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioCreacionDTO);

            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();

            return new Ejecucion
            {
                Exitoso = true,
                Mensaje = "Se ha registrado el usuario correctamente.",
                Id = usuario.Id_Usuario // Asegúrate de que este es el nombre correcto de la propiedad ID en tu entidad Usuario.
            };
        }

        public async Task<Ejecucion> CrearPermisosUsuario(List<PermisoUsuario> permisos_usuario)
        {
            Ejecucion ejecucion = new Ejecucion();

            try
            {
                foreach (var permiso_usuario in permisos_usuario)
                {
                    ejecucion = SetPermisoUsuario(permiso_usuario);
                }

                await _contexto.SaveChangesAsync(); // Guarda los cambios en la base de datos.

                return new Ejecucion
                {
                    Exitoso = true,
                    Mensaje = "Se han registrado los permisos del usuario correctamente.",
                    Id = null
                };
            }
            catch (Exception ex)
            {
                // Considera loguear la excepción para un análisis más detallado
                return new Ejecucion
                {
                    Exitoso = false,
                    Mensaje = $"Error al registrar los permisos del usuario: {ex.Message}",
                    Id = null
                };
            }

           
        }

        public async Task<Ejecucion> EliminarUsuario(int id_usuario)
        {
            // Buscar el usuario por ID.
            var usuario = await _contexto.Usuarios.FindAsync(id_usuario);

            // Verificar si el usuario existe.
            if (usuario == null)
            {
                return new Ejecucion
                {
                    Exitoso = false,
                    Mensaje = "Usuario no encontrado."
                };
            }

            try
            {
                usuario.Activo = false;

                // Eliminar el usuario del contexto.
                _contexto.Usuarios.Update(usuario);

                // Guardar los cambios en la base de datos.
                await _contexto.SaveChangesAsync();

                return new Ejecucion
                {
                    Exitoso = true,
                    Mensaje = "Usuario eliminado con éxito."
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción (opcionalmente, loguear el error)
                return new Ejecucion
                {
                    Exitoso = false,
                    Mensaje = $"Error al eliminar el usuario: {ex.Message}"
                };
            }
        }

        public async Task<Ejecucion> ActualizarUsuario(UsuarioActualizacionDTO usuarioActualizacionDTO)
        {
            var usuario = await _contexto.Usuarios.FindAsync(usuarioActualizacionDTO.Id_Usuario);
            if (usuario == null)
            {
                return new Ejecucion { Exitoso = false, Mensaje = "Usuario no encontrado." };
            }

            // Aquí, AutoMapper mapea las propiedades del DTO al usuario existente.
            // Asegúrate de configurar correctamente el mapeo para la clave si es necesario.
            _mapper.Map(usuarioActualizacionDTO, usuario);

            // La fecha de modificación se puede establecer automáticamente aquí o gestionarla a través del DTO.
            usuario.Fecha_Modificacion = DateTime.Now;

            _contexto.Usuarios.Update(usuario);

            try
            {
                await _contexto.SaveChangesAsync();
                return new Ejecucion { Exitoso = true, Mensaje = "Usuario actualizado correctamente.", Id = usuario.Id_Usuario };
            }
            catch (Exception ex)
            {
                // Manejo de cualquier excepción que pueda ocurrir durante la actualización.
                // Loguear el error puede ser útil para el diagnóstico.
                return new Ejecucion { Exitoso = false, Mensaje = $"Error al actualizar el usuario: {ex.Message}" };
            }
        }
    }
}
