using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Domain.Entities;
using MediatR;

namespace BankAppTestBack.Application.UseCases.Client.Commands.UpdateClient
{
    public record UpdateClientCommand : IRequest<ClientResponse>
    {
        public long ClientId { get; init; }
        public string? Name { get; init; }
        public EGender? Gender { get; init; }
        public DateTime? Birthdate { get; init; }
        public string? PersonId { get; init; }
        public string? Address { get; init; }
        public string? Phone { get; init; }
        public string? NewPassword { get; init; }
        public bool? State { get; init; }
    }
}
