using MediatR;

namespace BankAppTestBack.Application.UseCases.Movement.Commands.DeleteMovement
{
    public record DeleteMovementCommand(long MovementId) : IRequest<Unit>;
}
