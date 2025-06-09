using AutoMapper;
using elaw_desenvolvedor_junior.Application.Dtos;
using elaw_desenvolvedor_junior.Domain.Entities;
using elaw_desenvolvedor_junior.Domain.ValueObjects;

namespace elaw_desenvolvedor_junior.Application.Mapping
{
    public class MappingClient : Profile
    {
        public MappingClient() 
        {
            CreateMap<Client, GetClientDto>()
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                           opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email,
                           opt => opt.MapFrom(src => src.Email.EmailAddress))
                .ForMember(dest => dest.Phone,
                           opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address,
                           opt => opt.MapFrom(src => src.Address));

            CreateMap<CreateClientDto, Client>()
                .ConstructUsing((src, ctx) => new Client(
                    src.Name,
                    src.Email,
                    src.Phone
                ))
                .ForMember(dest => dest.Address,
                           opt => opt.MapFrom(src => src.Address));

            CreateMap<AddressDto, Address>()
                .ConstructUsing((src, ctx) => new Address(
                    src.Street,
                    src.Number,
                    src.City,
                    src.State,
                    src.ZipCode
                ));

            CreateMap<Address, AddressDto>();
        }
    }
}
