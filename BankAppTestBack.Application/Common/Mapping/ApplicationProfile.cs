using AutoMapper;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.Common.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Domain.Entities.Client, ClientResponse>();
            CreateMap<Domain.Entities.Account, AccountResponse>();
            CreateMap<Domain.Entities.Movement, MovementResponse>();
        }

    }
}
