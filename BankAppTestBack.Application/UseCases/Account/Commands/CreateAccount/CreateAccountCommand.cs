using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Domain.Entities;
using MediatR;

namespace BankAppTestBack.Application.UseCases.Account.Commands.CreateAccount
{
    public record CreateAccountCommand : IRequest<AccountResponse>
    {
        public string Number { get; init; }
        public EAccountType Type { get; init; }
        public double InitBalance { get; init; }
        public bool State { get; init; }
        public long ClientId { get; init; }
    }
}
