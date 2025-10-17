using MediatR;
using AutoMapper;
using BankAppTestBack.Application.Common.ValidationHandle.Exceptions;
using BankAppTestBack.Domain.Exceptions;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Infrastructure.Repositories;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.UseCases.Account.Commands.UpdateAccount
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(
            IAccountRepository accountRepository,
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AccountResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindByIdAsync(request.AccountId, cancellationToken);
            if (account == null)
            {
                throw new NotFoundException($"AccountId {request.AccountId} not found", "ACCOUNT_NOT_FOUND");
            }

            if (!string.IsNullOrWhiteSpace(request.Number) && request.Number != account.Number)
            {
                var numberExists = await _accountRepository.ExistsByNumberAsync(request.Number, cancellationToken);
                if (numberExists)
                {
                    throw new BusinessValidationException("ACCOUNT_NUMBER_ALREADY_EXIST");
                }
            }

            if (request.ClientId.HasValue && request.ClientId.Value != account.ClientId)
            {
                var clientExists = await _clientRepository.ExistsByIdAsync(request.ClientId.Value, cancellationToken);
                if (!clientExists)
                {
                    throw new NotFoundException("CLIENT_NOT_FOUND");
                }
            }

            account.Update(
                request.Number,
                request.Type,
                request.InitBalance,
                request.State,
                request.ClientId
            );
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AccountResponse>(account);
        }
    }
}
