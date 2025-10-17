using AutoMapper;
using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Application.Common.ValidationHandle.Exceptions;
using BankAppTestBack.Domain.Exceptions;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.UseCases.Account.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(
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

        public async Task<AccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {

            var numberExists = await _accountRepository.ExistsByNumberAsync(request.Number, cancellationToken);
            if (numberExists)
            {
                throw new BusinessValidationException("ACCOUNT_NUMBER_ALREADY_EXIST");
            }

            var clientExists = await _clientRepository.ExistsByIdAsync(request.ClientId, cancellationToken);
            if (!clientExists)
            {
                throw new NotFoundException($"ClientId {request.ClientId} not found", "CLIENT_NOT_FOUND");
            }

            var account = new Domain.Entities.Account
            {
                Number = request.Number,
                Type = request.Type,
                InitBalance = request.InitBalance,
                State = request.State,
                ClientId = request.ClientId
            };

            if (!account.IsValid())
            {
                throw new BusinessValidationException("INVALID_REQUEST");
            }

            _accountRepository.Add(account);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AccountResponse>(account);
        }
    }
}
