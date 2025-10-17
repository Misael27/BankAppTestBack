using AutoMapper;
using BankAppTestBack.Application.Client.Queries.GetAllClients.BankAppTestBack.Application.Client.Queries.GetAllClients;
using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Domain.Repositories;
using MediatR;

namespace BankAppTestBack.Application.UseCases.Client.Queries.GetAllClients
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, List<ClientResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetAllClientsQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<List<ClientResponse>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.FindAllAsync(cancellationToken);
            return _mapper.Map<List<ClientResponse>>(clients);
        }
    }
}
