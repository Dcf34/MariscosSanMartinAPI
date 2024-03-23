using AutoMapper;
using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Handlers;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MariscosSanMartinAPI.Features.Comidas
{
    public class ComidasRepository
    {
        private readonly AppDBContext _contexto;
        private readonly IMapper _mapper;

        public ComidasRepository(AppDBContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        internal static List<Comida> GetComidas(FiltroComida filtros)
        {
            var spParams = ComidasDB.GetComidasParams(filtros);

            try
            {
                var comidas = DapperHandler.GetFromProcedure<Comida>(ComidasDB.GetComidas, spParams);

                return comidas;
            }
            catch (Exception ex)
            {
                string parametros = DapperHandler.GetValuesFromInputParameters(spParams);
                //ExceptionHandler.InsertLog(ExceptionHandler.ConvertToLog(ex, null, parametros));

                throw;
            }
        }

        internal static Ejecucion SetComida(Comida comida)
        {

            var spParams = ComidasDB.SetComidaParams(comida);

            try
            {
                var ejecucion = DapperHandler.SetFromProcedure(ComidasDB.SetComida, spParams);

                return ejecucion;
            }
            catch (Exception ex)
            {
                string parametros = DapperHandler.GetValuesFromInputParameters(spParams);

                return new()
                {
                    Exitoso = false,
                    Mensaje = ex.Message,
                };
                throw;
            }
        }
    }
}
