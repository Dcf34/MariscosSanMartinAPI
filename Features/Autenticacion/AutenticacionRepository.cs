using System.DirectoryServices.AccountManagement;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using MariscosSanMartinAPI.Features.Autenticacion;
using MariscosSanMartinAPI.Handlers;
using MariscosSanMartinAPI;
using MariscosSanMartinAPI.Features.Usuarios;

namespace MariscosSanMartinAPI.Features.Autenticacion
{
    public class AutenticacionRepository
    {
        private readonly UsuariosRepository _usuariosRepository;

        public AutenticacionRepository(UsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }
        private static Autenticacion UsuarioNoExiste { get; } = new() { Estatus = EstatusAutenticacion.USUARIO_NO_EXISTE, Mensaje = "El usuario no existe." };
        private static Autenticacion UsuarioInactivo { get; } = new() { Estatus = EstatusAutenticacion.USUARIO_INACTIVO, Mensaje = "El usuario se encuentra inactivo." };
        private static Autenticacion UsuarioAutenticado { get; } = new() { Estatus = EstatusAutenticacion.AUTENTICADO, Mensaje = "Usuario y contraseña correctos." };
        private static Autenticacion UsuarioNoAutenticado { get; } = new() { Estatus = EstatusAutenticacion.NO_AUTENTICADO, Mensaje = "Usuario y/o contraseña incorrectos." };
        internal async Task<List<Usuario>> ObtenerUsuarios(string usuario)
        {
            return await _usuariosRepository.GetUsuariosPorCorreo(usuario);
        }

        internal async Task<Autenticacion> Login(Data sesion)
        {
            try
            {
                // Obtener el usuario por correo
                var usuarios = await ObtenerUsuarios(sesion.Sesion.Usuario ?? "");

                if (!usuarios.Any()) return UsuarioNoExiste;

                var usuario = usuarios.First();

                if (!usuario.Activo) return UsuarioInactivo;

                bool esAutenticacionCorrecta = false;

                if (usuario != null)
                {
                    string password = StringHandler.Base64Decode(sesion.Sesion.Clave ?? "");

                    if (password == StringHandler.Base64Decode(usuario.Clave ?? ""))
                    {
                        esAutenticacionCorrecta = true;
                    }
                }

                if (!esAutenticacionCorrecta) return UsuarioNoAutenticado;

                UsuarioAutenticado.Token = GetToken(usuario ?? new Usuario { });

                return UsuarioAutenticado;
            }
            catch (Exception ex)
            {
                //LogRepository.InsertLog(LogRepository.ConvertToLog(ex));

                return new Autenticacion();
            }
        }

        private static string GetToken(Usuario usuario)
        {
            var secretKey = JsonConfiguration.AppSetting["SecretKey"] ?? "";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);

            //Convertir en formato JSON la información del usuario como sus permisos
            string usuarioJson = JsonSerializer.Serialize(usuario);

            var claims = new Claim[] {
                new("usuario", usuarioJson, JsonClaimValueTypes.Json)
            };

            //Generar el payload del token
            JwtPayload payload = new(
                issuer: "Mariscos_API",
                audience: "USERS",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(24)
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
