using MediatR;
using BankAppTestBack.Domain.Entities;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.UseCases.Account.Commands.UpdateAccount
{
    public record UpdateAccountCommand : IRequest<AccountResponse>
    {
        public long AccountId { get; init; }
        public string? Number { get; init; }
        public EAccountType? Type { get; init; }
        public double? InitBalance { get; init; }
        public bool? State { get; init; }
        public long? ClientId { get; init; }
    }
}