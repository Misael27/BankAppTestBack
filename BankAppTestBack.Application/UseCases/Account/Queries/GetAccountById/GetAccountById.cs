using BankAppTestBack.Application.Dtos;
using MediatR;

namespace BankAppTestBack.Application.Account.Queries
{
    namespace BankAppTestBack.Application.Account.Queries.GetAccountById
    {
        public record GetAccountByIdQuery(long AccountId) : IRequest<AccountResponse>;
    }
}
