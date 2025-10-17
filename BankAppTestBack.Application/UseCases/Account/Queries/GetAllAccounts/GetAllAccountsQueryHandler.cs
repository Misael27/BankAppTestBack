using MediatR;
using AutoMapper;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.UseCases.Account.Queries.GetAllAccounts
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountResponse>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAllAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<List<AccountResponse>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.FindAllAsync(cancellationToken);
            return _mapper.Map<List<AccountResponse>>(accounts);
        }
    }
}
