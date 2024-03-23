using AutoMapper;
using MariscosSanMartinAPI.Handlers;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MariscosSanMartinAPI.Features.Clientes
{
    public class ClientesRepository
    {
        private readonly AppDBContext _contexto;
        private readonly IMapper _mapper;

        public ClientesRepository(AppDBContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        internal static List<Cliente> GetClientes(FiltroCliente filtros)
        {
            var spParams = ClientesDB.GetClientesParams(filtros);

            try
            {
                var clientes = DapperHandler.GetFromProcedure<Cliente>(ClientesDB.spGetClientes, spParams);

                return clientes;
            }
            catch (Exception ex)
            {
                string parametros = DapperHandler.GetValuesFromInputParameters(spParams);
                //ExceptionHandler.InsertLog(ExceptionHandler.ConvertToLog(ex, null, parametros));

                throw;
            }
        }

        internal static Ejecucion SetCliente(Cliente cliente)
        {

            var spParams = ClientesDB.SetClienteParams(cliente);

            try
            {
                var ejecucion = DapperHandler.SetFromProcedure(ClientesDB.spSetCliente, spParams);

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

            }
        }
    }
}
