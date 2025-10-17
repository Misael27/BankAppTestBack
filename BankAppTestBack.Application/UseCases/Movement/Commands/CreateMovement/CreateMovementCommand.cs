using MediatR;
using BankAppTestBack.Domain.Entities;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.UseCases.Movement.Commands.CreateMovement
{
    public record CreateMovementCommand : IRequest<MovementResponse>
    {
        public EMovementType Type { get; init; }
        public double Value { get; init; }
        public long AccountId { get; init; }
    }
}
