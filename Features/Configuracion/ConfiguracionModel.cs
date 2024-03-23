using System.ComponentModel.DataAnnotations;

namespace MariscosSanMartinAPI.Features.Configuracion
{
    public class Configuracion
    {
        [Key]
        public int? Id_Configuracion { get; set; }
        public bool Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public string? Nombre { get; set; } = "";
        public string? Telefono { get; set; } = "";
        public string? Correo { get; set; } = "";
        public string? Direccion { get; set; } = "";

    }
}
