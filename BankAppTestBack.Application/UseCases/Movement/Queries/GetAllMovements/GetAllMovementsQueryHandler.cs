using MediatR;
using AutoMapper;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Application.Dtos;

namespace BankAppTestBack.Application.UseCases.Movement.Queries.GetAllMovements
{
    public class GetAllMovementsQueryHandler : IRequestHandler<GetAllMovementsQuery, List<MovementResponse>>
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IMapper _mapper;

        public GetAllMovementsQueryHandler(IMovementRepository movementRepository, IMapper mapper)
        {
            _movementRepository = movementRepository;
            _mapper = mapper;
        }

        public async Task<List<MovementResponse>> Handle(GetAllMovementsQuery request, CancellationToken cancellationToken)
        {
            var movements = await _movementRepository.FindAllAsync(cancellationToken);
            return _mapper.Map<List<MovementResponse>>(movements);
        }
    }
}