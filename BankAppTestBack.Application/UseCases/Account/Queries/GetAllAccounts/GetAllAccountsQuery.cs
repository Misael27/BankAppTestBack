using MediatR;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.UseCases.Account.Queries.GetAllAccounts
{
    public record GetAllAccountsQuery : IRequest<List<AccountResponse>>;
}