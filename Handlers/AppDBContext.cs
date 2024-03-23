using Azure;
using MariscosSanMartinAPI.Features.Comidas;
using MariscosSanMartinAPI.Features.Configuracion;
using MariscosSanMartinAPI.Features.Permisos;
using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Features.Ventas;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Algorithm;

namespace MariscosSanMartinAPI.Handlers
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> opciones) : base(opciones)
        {

        }

        //Entidades / Entities
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<PermisoUsuario> Permisos_Usuarios { get; set; }
        public DbSet<Configuracion> Configuracion { get; set; }
        public DbSet<Comida> Comidas { get; set; }
        public DbSet<Venta> Ventas { get; set; }
    }
}

