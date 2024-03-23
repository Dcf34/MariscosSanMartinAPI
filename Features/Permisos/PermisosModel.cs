using System.ComponentModel.DataAnnotations;

namespace MariscosSanMartinAPI.Features.Permisos
{
    public class Permiso
    {
        [Required]
        [Key]
        public int? Id_Permiso { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Nombre { get; set; } = "";
    }
}
