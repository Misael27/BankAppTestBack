using AutoMapper;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.Common.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Domain.Entities.Client, ClientResponse>();
            CreateMap<Domain.Entities.Account, AccountResponse>()
            .ForMember(
                dest => dest.clientName,
                opt => opt.MapFrom(src => src.Client.Name)
            );
            CreateMap<Domain.Entities.Movement, MovementResponse>();
        }

    }
}
