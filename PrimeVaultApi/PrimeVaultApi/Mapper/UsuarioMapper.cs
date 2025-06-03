using AutoMapper;
using PrimeVaultApi.DTOs;

namespace PrimeVaultApi.Mapper;

public class UsuarioMapper : Profile
{
    public UsuarioMapper()
    {
        CreateMap<Usuario, UsuarioCriarDto>();
        CreateMap<UsuarioCriarDto, Usuario>();
        CreateMap<Usuario, UsuarioEditDto>();
    }
}



