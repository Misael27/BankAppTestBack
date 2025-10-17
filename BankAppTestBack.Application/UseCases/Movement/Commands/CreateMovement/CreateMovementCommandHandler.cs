using AutoMapper;
using BankAppTestBack.Application.Common.Options;
using BankAppTestBack.Application.Common.ValidationHandle.Exceptions;
using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Domain.Entities;
using BankAppTestBack.Domain.Exceptions;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Options;

namespace BankAppTestBack.Application.UseCases.Movement.Commands.CreateMovement
{
    public class CreateMovementCommandHandler : IRequestHandler<CreateMovementCommand, MovementResponse>
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly double _maxDailyWithdrawal;

        public CreateMovementCommandHandler(
            IMovementRepository movementRepository,
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<LimitOptions> limitOptions)
        {
            _movementRepository = movementRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _maxDailyWithdrawal = limitOptions.Value.MaxDailyWithdrawal;
        }

        private async Task<double> GetAccountBalanceAsync(long accountId, CancellationToken cancellationToken)
        {
            var lastMovement = await _movementRepository.FindLastMovementByAccountIdAsync(accountId, cancellationToken);
            if (lastMovement != null)
            {
                return lastMovement.Balance;
            }

            var account = await _accountRepository.FindByIdAsync(accountId, cancellationToken);
            if (account != null)
            {
                return account.InitBalance;
            }

            throw new NotFoundException("ACCOUNT_NOT_FOUND");
        }


        public async Task<MovementResponse> Handle(CreateMovementCommand request, CancellationToken cancellationToken)
        {
            var accountExists = await _accountRepository.ExistsByIdAsync(request.AccountId, cancellationToken);
            if (!accountExists)
            {
                throw new NotFoundException($"AccountId {request.AccountId} not found", "ACCOUNT_NOT_FOUND");
            }

            var movement = new Domain.Entities.Movement
            {
                Type = request.Type,
                Value = request.Value,
                AccountId = request.AccountId
            };

            if (!movement.IsValid())
            {
                throw new BusinessValidationException("INVALID_REQUEST");
            }

            double currentAccountBalance = await GetAccountBalanceAsync(request.AccountId, cancellationToken);
            movement.AddBalance(currentAccountBalance);

            if (!movement.IsBalanceValid())
            {
                throw new BusinessValidationException("SALDO_NO_DISPONIBLE");
            }

            if (movement.Type == EMovementType.Retiro)
            {
                double todayDebitTotal = await _movementRepository.GetTodayDebitTotalAsync(request.AccountId, cancellationToken);
                if (!movement.CanDebitToday(todayDebitTotal, _maxDailyWithdrawal))
                {
                    throw new BusinessValidationException("CUPO_DIARIO_EXCEDIDO");
                }
            }

            _movementRepository.Add(movement);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MovementResponse>(movement);
        }
    }
}