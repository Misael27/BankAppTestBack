using MediatR;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.UseCases.Movement.Queries.GetMovementById
{
    public record GetMovementByIdQuery(long MovementId) : IRequest<MovementResponse>;
}