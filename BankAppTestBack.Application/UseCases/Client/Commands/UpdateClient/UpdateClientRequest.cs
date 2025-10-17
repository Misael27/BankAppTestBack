using BankAppTestBack.Domain.Entities;

namespace BankAppTestBack.Application.UseCases.Client.Commands.UpdateClient
{
    public record UpdateClientRequest
    {
        public string? Name { get; init; }

        public EGender Gender { get; init; }

        public DateTime? Birthdate { get; init; }

        public string? PersonId { get; init; }

        public string? Address { get; init; }

        public string? Phone { get; init; }

        public string? Password { get; init; }

        public bool? State { get; init; }
    }
}
