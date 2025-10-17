using BankAppTestBack.Application.Common.ValidationHandle.Exceptions;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Infrastructure.Repositories;
using MediatR;

namespace BankAppTestBack.Application.UseCases.Movement.Commands.DeleteMovement
{
    public class DeleteMovementCommandHandler : IRequestHandler<DeleteMovementCommand, Unit>
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMovementCommandHandler(IMovementRepository movementRepository, IUnitOfWork unitOfWork)
        {
            _movementRepository = movementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteMovementCommand request, CancellationToken cancellationToken)
        {
            var movement = await _movementRepository.FindByIdAsync(request.MovementId, cancellationToken);
            if (movement == null)
            {
                throw new NotFoundException("MOVEMENT_NOT_FOUND");
            }
            _movementRepository.Remove(movement);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}