using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace MariscosSanMartinAPI.Features.Usuarios
{
    public class Usuario
    {
        [Required]
        [Key]
        public int? Id_Usuario { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public DateTime? Fecha_Modificacion { get; set; }
        [Required]
        public int? Id_Usuario_Modificacion { get; set; }
        [Required]
        public DateTime? Fecha_Creacion { get; set; }
        [Required]
        public int? Id_Usuario_Creacion { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Nombre { get; set; } = "";
        [Required]
        [MaxLength(150)]
        public string? Correo { get; set; } = "";
        [Required]
        [MaxLength(20)]
        public string? Telefono { get; set; } = "";
        [Required]
        [MaxLength(150)]
        public string? Cuenta_Usuario { get; set; } = "";
        [MaxLength(50)]
        public string? Clave { get; set; } = "";
    }
    public class UsuarioDTO
    {
        public int? Id_Usuario { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public string? Nombre { get; set; } = "";
        public string? Correo { get; set; } = "";
        public string? Cuenta_Usuario { get; set; } = "";
        public string? Telefono { get; set; } = "";

    }

    public class UsuarioPerfil
    {
        public int? Id_Usuario { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public string? Nombre { get; set; } = "";
        public string? Correo { get; set; } = "";
        public string? Cuenta_Usuario { get; set; } = "";
        public string? Telefono { get; set; } = "";
        public string? Clave { get; set; } = "";
    }
    public class FiltroUsuario
    {
        public int? Id_Usuario { get; set; }
        public bool? Activo { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Cuenta_Usuario { get; set; }
    }

    public class UsuarioCreacionDTO
    {
        public bool? Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public string? Nombre { get; set; } = "";
        public string? Correo { get; set; } = "";
        public string? Cuenta_Usuario { get; set; } = "";
        public string? Clave { get; set; } = "";
        public string? Telefono { get; set; } = "";
    }

    public class UsuarioActualizacionDTO
    {
        public int? Id_Usuario { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public string? Nombre { get; set; } = "";
        public string? Correo { get; set; } = "";
        public string? Cuenta_Usuario { get; set; } = "";
        public string? Clave { get; set; } = "";
        public string? Telefono { get; set; } = "";

    }

    public class PermisoUsuario
    {
        [Key]
        public int? Id_Permiso_Usuario { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public int? Id_Permiso { get; set; }
        public int? Id_Usuario { get; set; }
    }

    public class PermisoUsuarioCreacionDTO
    {
        public bool? Activo { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public int? Id_Permiso { get; set; }
        public int? Id_Usuario { get; set; }
    }
}
