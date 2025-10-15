using AutoMapper;
using BankAppTestBack.Domain.Exceptions;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Domain.Services;
using BankAppTestBack.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.Client.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IMapper mapper
        )
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public async Task<ClientResponse> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var personIdExists = await _clientRepository.ExistsByPersonIdAsync(request.PersonId);
            if (personIdExists)
            {
                throw new BusinessValidationException("PERSON_ID_ALREADY_EXIST");
            }

            string hashedPassword = _passwordHasher.HashPassword(request.Password);

            var client = new Domain.Entities.Client(
                request.Name,
                request.Gender,
                request.Birthdate,
                request.PersonId,
                request.Address,
                request.Phone,
                hashedPassword,
                request.State
            );

            if (!client.IsValid())
            {
                throw new BusinessValidationException("INVALID_REQUEST");
            }

            _clientRepository.Add(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ClientResponse>(client);
        }
    }
}
