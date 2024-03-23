namespace MariscosSanMartinAPI.Features.Reportes
{
    public class ContenidoArchivo
    {
        public string Nombre { get; set; } = "";

        public string Tipo_Contenido { get; set; } = "";

        public MemoryStream Contenido { get; set; } = new();

        public bool Exitoso { get; set; }

        public string Mensaje { get; set; } = "";
    }

    public enum TipoReporte
    {
        Ventas = 1,
        Pedidos = 2,
        Clientes = 3,
        Comidas = 4,
        Usuario = 5
    }
}
