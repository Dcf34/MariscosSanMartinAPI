using AutoMapper;
using MariscosSanMartinAPI.Features.Configuracion;
using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Handlers;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MariscosSanMartinAPI.Features.Configuracion
{
    public class ConfiguracionRepository
    {
        private readonly AppDBContext _contexto;
        private readonly IMapper _mapper;

        public ConfiguracionRepository(AppDBContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        public async Task<List<Configuracion>> GetDatosEmpresa()
        {
            var empresas = await _contexto.Configuracion.ToListAsync();

            if(empresas != null)
            {
                return empresas;
            }
            else
            {
                return new List<Configuracion>();
            }

        }

        public async Task<Ejecucion> ActualizarConfiguracion(Configuracion configuracion)
        {
            Ejecucion ejecucion = new Ejecucion { Exitoso = false, Mensaje = "Empresa no encontrada." };

            configuracion.Fecha_Modificacion = DateTime.Now;

            _contexto.Configuracion.Update(configuracion);

            try
            {
                await _contexto.SaveChangesAsync();
                return new Ejecucion { Exitoso = true, Mensaje = "Los datos de la empresa han sido actualizados correctamente.", Id = configuracion.Id_Configuracion };
            }
            catch (Exception ex)
            {
                return new Ejecucion { Exitoso = false, Mensaje = $"Error al actualizar el usuario: {ex.Message}" };
            }
        }
    }
}
