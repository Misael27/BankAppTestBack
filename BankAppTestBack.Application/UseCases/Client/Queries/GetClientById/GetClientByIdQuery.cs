using BankAppTestBack.Application.Dtos;
using MediatR;

namespace BankAppTestBack.Application.Client.Queries.GetClientById
{
    namespace BankAppTestBack.Application.Client.Queries.GetClientById
    {
        public record GetClientByIdQuery(long ClientId) : IRequest<ClientResponse>;
    }
}
