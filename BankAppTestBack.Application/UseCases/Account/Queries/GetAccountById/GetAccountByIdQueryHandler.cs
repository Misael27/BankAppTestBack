using AutoMapper;
using BankAppTestBack.Application.Account.Queries.BankAppTestBack.Application.Account.Queries.GetAccountById;
using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Application.Common.ValidationHandle.Exceptions;
using BankAppTestBack.Domain.Repositories;
using MediatR;

namespace BankAppTestBack.Application.UseCases.Account.Queries.GetAccountById
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountResponse> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindByIdAsync(request.AccountId, cancellationToken);

            if (account == null)
            {
                throw new NotFoundException($"AccountId {request.AccountId} not found", "ACCOUNT_NOT_FOUND");
            }

            return _mapper.Map<AccountResponse>(account);
        }
    }
}
