using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MariscosSanMartinAPI.Features.Ventas
{
    public class Venta
    {
        [Key]
        public int? Id_Venta { get; set; }
        public bool Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public int? Id_Cliente { get; set; }
        public string? Codigo_Venta { get; set; } = "";
        public DateTime? Fecha_Venta { get; set; }
        public decimal? Total { get; set; }
    }
    public class VentaDAO
    {
        public int? Id_Venta { get; set; }
        public bool Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public int? Id_Cliente { get; set; }
        public string? Nombre_Cliente { get; set; }
        public string? Codigo_Venta { get; set; } = "";
        public DateTime? Fecha_Venta { get; set; }
        public decimal? Total { get; set; }
    }

    public class VentaDTO
    {
        public bool Activo { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public int? Id_Cliente { get; set; }
        public DateTime? Fecha_Venta { get; set; }
        public decimal? Total { get; set; }
        public string? Codigo_Venta { get; set; }
        public DetalleVentaDTO[]? Detalles_Venta { get; set; }

    }

    public class DetalleVentaDAO
    {
        public int? Id_Detalle_Venta { get; set; }
        public bool Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public int? Id_Venta { get; set; }
        public int? Id_Comida { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public bool? Aplica_Desc { get; set; }
        public int? Descuento { get; set; }
        public decimal? Subtotal { get; set; }
        public int? Orden { get; set; }

    }

    public class DetalleVentaDTO
    {
        public bool Activo { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public int? Id_Venta { get; set; }
        public int? Id_Comida { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public bool? Aplica_Desc { get; set; }
        public int? Descuento { get; set; }
        public decimal? Subtotal { get; set; }
    }

    public class FiltroVentas
    {
        public int? Id_Venta { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Desde { get; set; }
        public DateTime? Fecha_Hasta { get; set; }
        public int? Id_Cliente { get; set; }
        public decimal? Total_Desde { get; set; }
        public decimal? Total_Hasta { get; set; }
    }

    public class FiltroDetalleVenta
    {
        public int? Id_Venta { get; set; }
        public bool? Activo { get; set; }
    }

}
