using AutoMapper;
using BankAppTestBack.Application.Client.Commands.CreateClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Domain.Entities.Client, ClientResponse>();
        }

    }
}
