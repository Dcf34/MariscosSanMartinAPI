namespace MariscosSanMartinAPI.Features.Autenticacion
{
    public enum EstatusAutenticacion
    {
        AUTENTICADO = 1,
        NO_AUTENTICADO = 2,
        USUARIO_NO_EXISTE = 3,
        USUARIO_INACTIVO = 4
    }

    public class Data
    {
        public required Sesion Sesion { get; set; }
    }
    public class Autenticacion
    {
        public EstatusAutenticacion Estatus { get; set; }
        public string Mensaje { get; set; } = "";
        public string Token { get; set; } = "";
    }

    public class Sesion
    {
        public string? Usuario { get; set; }

        public string? Clave { get; set; }
    }
}
