using AutoMapper;
using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Application.Common.ValidationHandle.Exceptions;
using BankAppTestBack.Domain.Exceptions;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Domain.Services;
using BankAppTestBack.Infrastructure.Repositories;
using MediatR;

namespace BankAppTestBack.Application.UseCases.Client.Commands.UpdateClient
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<ClientResponse> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.FindByIdAsync(request.ClientId, cancellationToken);
            if (client == null)
            {
                throw new NotFoundException("CLIENT_NOT_FOUND");
            }

            if (!string.IsNullOrWhiteSpace(request.PersonId) && request.PersonId != client.PersonId)
            {
                var personIdExists = await _clientRepository.ExistsByPersonIdAsync(request.PersonId, cancellationToken);
                if (personIdExists)
                {
                    throw new BusinessValidationException("PERSON_ID_ALREADY_EXIST");
                }
            }

            string? newHashedPassword = null;
            if (!string.IsNullOrWhiteSpace(request.NewPassword))
            {
                newHashedPassword = _passwordHasher.HashPassword(request.NewPassword);
            }

            client.Update(
                request.Name,
                request.Gender,
                request.Birthdate,
                request.PersonId,
                request.Address,
                request.Phone,
                request.State,
                newHashedPassword
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ClientResponse>(client);
        }
    }
}
