using AutoMapper;
using MariscosSanMartinAPI.Features.Permisos;
using MariscosSanMartinAPI.Features.Usuarios;
using MariscosSanMartinAPI.Handlers;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MariscosSanMartinAPI.Features.Permisos
{
    public class PermisosRepository
    {
        private readonly AppDBContext _contexto;
        private readonly IMapper _mapper;

        public PermisosRepository(AppDBContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }
        public async Task<List<Permiso>> GetPermisos()
        {
            return await _contexto.Permisos.ToListAsync();
        }
    }
}
