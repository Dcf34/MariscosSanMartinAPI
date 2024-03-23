using System.ComponentModel.DataAnnotations;

namespace MariscosSanMartinAPI.Features.Pedidos
{
    public class PedidoDAO
    {
        [Key]
        public int? Id_Pedido { get; set; }
        public bool Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public int? Id_Cliente { get; set; }
        public string? Nombre_Cliente { get; set; }
        public int? Id_Venta { get; set; }
        public decimal? Total { get; set; }
    }

    public class FiltroPedidos
    {
        public int? Id_Pedido { get; set; }
        public bool? Activo { get; set; }
        public int? Id_Cliente { get; set; }
        public decimal? Total_Desde { get; set; }
        public decimal? Total_Hasta { get; set; }
    }

    public class PedidoDTO
    {
        public bool Activo { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public int? Id_Cliente { get; set; }
        public int? Id_Venta { get; set; }
    }
}
