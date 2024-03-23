using MariscosSanMartinAPI.Features.Autenticacion;
using MariscosSanMartinAPI.Features.Clientes;
using MariscosSanMartinAPI.Features.Comidas;
using MariscosSanMartinAPI.Features.Configuracion;
using MariscosSanMartinAPI.Features.Pedidos;
using MariscosSanMartinAPI.Features.Permisos;
using MariscosSanMartinAPI.Features.Reportes;
using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Features.Ventas;
using MariscosSanMartinAPI.Handlers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace MariscosSanMartinAPI
{
    public static class ServiceRegistrations
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Registra los repositorios y controladores directamente
            services.AddScoped<AutenticacionRepository>();
            services.AddScoped<UsuariosRepository>();
            services.AddScoped<PermisosRepository>();
            services.AddScoped<ConfiguracionRepository>();
            services.AddScoped<ComidasRepository>();
            services.AddScoped<ClientesRepository>();
            services.AddScoped<VentasRepository>();
            services.AddScoped<PedidosRepository>();
            services.AddScoped<ReportesRepository>();


            services.AddAutoMapper(typeof(Program).Assembly); // Asegúrate de referenciar correctamente la ubicación de tus perfiles.

            // Otros servicios que puedas necesitar registrar...

            // Configuración de la licencia de QuestPDF
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        }
    }
}
