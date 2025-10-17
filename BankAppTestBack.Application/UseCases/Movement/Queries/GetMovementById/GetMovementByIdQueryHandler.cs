using MediatR;
using AutoMapper;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Application.Common.ValidationHandle.Exceptions;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.UseCases.Movement.Queries.GetMovementById
{
    public class GetMovementByIdQueryHandler : IRequestHandler<GetMovementByIdQuery, MovementResponse>
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IMapper _mapper;

        public GetMovementByIdQueryHandler(IMovementRepository movementRepository, IMapper mapper)
        {
            _movementRepository = movementRepository;
            _mapper = mapper;
        }

        public async Task<MovementResponse> Handle(GetMovementByIdQuery request, CancellationToken cancellationToken)
        {
            var movement = await _movementRepository.FindByIdAsync(request.MovementId, cancellationToken);

            if (movement == null)
            {
                throw new NotFoundException("MOVEMENT_NOT_FOUND");
            }

            return _mapper.Map<MovementResponse>(movement);
        }
    }
}