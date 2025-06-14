using AutoMapper;
using XPTO.Application.DTOs;
using XPTO.Domain.Entities;

namespace XPTO.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<Endereco, EnderecoDTO>().ReverseMap();
    }
}

