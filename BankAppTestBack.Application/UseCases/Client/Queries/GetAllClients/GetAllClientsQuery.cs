using BankAppTestBack.Application.Dtos;
using MediatR;

namespace BankAppTestBack.Application.Client.Queries.GetAllClients
{
    namespace BankAppTestBack.Application.Client.Queries.GetAllClients
    {
        public record GetAllClientsQuery : IRequest<List<ClientResponse>>;
    }
}
