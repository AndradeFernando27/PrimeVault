using AutoMapper;
using PrimeVaultApi.DTOs;
using PrimeVaultApi.Models;

namespace PrimeVaultApi.Mapper;

public class ContaMapper :Profile
{
    public ContaMapper() {
        CreateMap<Conta, ContaDto>();
        CreateMap<ContaDto, Conta>();
    }
}
