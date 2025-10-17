using BankAppTestBack.Application.Common.ValidationHandle.Exceptions;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Infrastructure.Repositories;
using MediatR;

namespace BankAppTestBack.Application.UseCases.Account.Commands.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindByIdAsync(request.AccountId, cancellationToken);
            if (account == null)
            {
                throw new NotFoundException("ACCOUNT_NOT_FOUND");
            }
            _accountRepository.Remove(account);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}