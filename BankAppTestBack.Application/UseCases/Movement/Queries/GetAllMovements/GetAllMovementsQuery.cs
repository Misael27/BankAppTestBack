using MediatR;
using System.Collections.Generic;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.UseCases.Movement.Queries.GetAllMovements
{
    public record GetAllMovementsQuery : IRequest<List<MovementResponse>>;
}
