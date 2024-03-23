using AutoMapper;
using MariscosSanMartinAPI.Features.Usuarios;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Mapeo de Usuario a UsuarioDTO
        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.Id_Usuario, act => act.MapFrom(src => src.Id_Usuario))
            .ForMember(dest => dest.Activo, act => act.MapFrom(src => src.Activo))
            .ForMember(dest => dest.Fecha_Modificacion, act => act.MapFrom(src => src.Fecha_Modificacion))
            .ForMember(dest => dest.Id_Usuario_Modificacion, act => act.MapFrom(src => src.Id_Usuario_Modificacion))
            .ForMember(dest => dest.Fecha_Creacion, act => act.MapFrom(src => src.Fecha_Creacion))
            .ForMember(dest => dest.Id_Usuario_Creacion, act => act.MapFrom(src => src.Id_Usuario_Creacion))
            .ForMember(dest => dest.Nombre, act => act.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Correo, act => act.MapFrom(src => src.Correo))
            .ForMember(dest => dest.Telefono, act => act.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.Cuenta_Usuario, act => act.MapFrom(src => src.Cuenta_Usuario))
            .ReverseMap(); // Si necesitas mapeo bidireccional

        // Mapeo de Usuario a UsuarioPerfil
        CreateMap<Usuario, UsuarioPerfil>();

        // Mapeo de UsuarioCreacionDTO a Usuario
        CreateMap<UsuarioCreacionDTO, Usuario>()
            .ForMember(dest => dest.Activo, act => act.MapFrom(src => src.Activo))
            .ForMember(dest => dest.Fecha_Modificacion, act => act.MapFrom(src => src.Fecha_Modificacion))
            .ForMember(dest => dest.Id_Usuario_Modificacion, act => act.MapFrom(src => src.Id_Usuario_Modificacion))
            .ForMember(dest => dest.Fecha_Creacion, act => act.MapFrom(src => src.Fecha_Creacion))
            .ForMember(dest => dest.Id_Usuario_Creacion, act => act.MapFrom(src => src.Id_Usuario_Creacion))
            .ForMember(dest => dest.Nombre, act => act.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Correo, act => act.MapFrom(src => src.Correo))
            .ForMember(dest => dest.Telefono, act => act.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.Cuenta_Usuario, act => act.MapFrom(src => src.Cuenta_Usuario))
            .ForMember(dest => dest.Clave, act => act.MapFrom(src => src.Clave));

        // Mapeo de UsuarioActualizacionDTO a Usuario (para actualización)
        CreateMap<UsuarioActualizacionDTO, Usuario>()
            .ForMember(dest => dest.Id_Usuario, act => act.MapFrom(src => src.Id_Usuario))
            .ForMember(dest => dest.Activo, act => act.MapFrom(src => src.Activo))
            .ForMember(dest => dest.Fecha_Modificacion, act => act.MapFrom(src => src.Fecha_Modificacion))
            .ForMember(dest => dest.Id_Usuario_Modificacion, act => act.MapFrom(src => src.Id_Usuario_Modificacion))
            .ForMember(dest => dest.Fecha_Creacion, act => act.Ignore()) // Ignora este campo si no debe actualizarse
            .ForMember(dest => dest.Id_Usuario_Creacion, act => act.Ignore()) // Ignora este campo si no debe actualizarse
            .ForMember(dest => dest.Nombre, act => act.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Correo, act => act.MapFrom(src => src.Correo))
            .ForMember(dest => dest.Telefono, act => act.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.Cuenta_Usuario, act => act.MapFrom(src => src.Cuenta_Usuario))
            .ForMember(dest => dest.Clave, act => act.MapFrom(src => src.Clave));

        CreateMap<PermisoUsuarioCreacionDTO, PermisoUsuario>()
            .ForMember(dest => dest.Id_Permiso_Usuario, act => act.Ignore())
            .ForMember(dest => dest.Activo, act => act.MapFrom(src => src.Activo))
            .ForMember(dest => dest.Fecha_Modificacion, act => act.MapFrom(src => src.Fecha_Modificacion))
            .ForMember(dest => dest.Id_Usuario_Modificacion, act => act.MapFrom(src => src.Id_Usuario_Modificacion))
            .ForMember(dest => dest.Fecha_Creacion, act => act.MapFrom(src => src.Fecha_Creacion))
            .ForMember(dest => dest.Id_Usuario_Creacion, act => act.MapFrom(src => src.Id_Usuario_Creacion))
            .ForMember(dest => dest.Id_Permiso, act => act.MapFrom(src => src.Id_Permiso))
            .ForMember(dest => dest.Id_Usuario, act => act.MapFrom(src => src.Id_Usuario))
            ;
    }
}