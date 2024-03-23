using System.ComponentModel.DataAnnotations;

namespace MariscosSanMartinAPI.Features.Comidas
{
    public class Comida
    {
        [Key]
        public int? Id_Comida { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public string? Codigo { get; set; } = "";
        public string? Nombre { get; set; } = "";
        public decimal? Precio { get; set; }
        public string? Descripcion { get; set; } = "";
    }

    public class FiltroComida
    {
        public int? Id_Comida { set; get; }
        public bool? Activo { set; get; }
    }
}
