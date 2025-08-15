using AutoMapper;
using GerenciarProcessos.Application.Dtos;
using GerenciarProcessos.Application.DTOs;
using GerenciarProcessos.Domain.Entities;

namespace GerenciarProcessos.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Cliente, ClienteDto>().ReverseMap();
        CreateMap<Cliente, CriarClienteDto>().ReverseMap();
        CreateMap<Processo, ProcessoDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Cliente.Id));

        CreateMap<CriarProcessoDto, Processo>();

    }
}
