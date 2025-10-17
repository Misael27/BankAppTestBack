using MediatR;

namespace BankAppTestBack.Application.UseCases.Account.Commands.DeleteAccount
{
    public record DeleteAccountCommand(long AccountId) : IRequest<Unit>;
}
