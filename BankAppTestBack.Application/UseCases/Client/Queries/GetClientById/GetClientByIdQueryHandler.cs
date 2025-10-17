using AutoMapper;
using BankAppTestBack.Application.Client.Queries.GetClientById.BankAppTestBack.Application.Client.Queries.GetClientById;
using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Application.Common.ValidationHandle.Exceptions;
using BankAppTestBack.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BankAppTestBack.Application.UseCases.Client.Queries.GetClientById
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.FindByIdAsync(request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException("CLIENT_NOT_FOUND");
            }

            return _mapper.Map<ClientResponse>(client);
        }
    }
}
