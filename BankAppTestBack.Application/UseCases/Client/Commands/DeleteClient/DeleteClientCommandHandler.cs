using BankAppTestBack.Application.Common.ValidationHandle.Exceptions;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.UseCases.Client.Commands.DeleteClient
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Unit>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.FindByIdAsync(request.ClientId, cancellationToken);
            if (client == null)
            {
                throw new NotFoundException("CLIENT_NOT_FOUND");
            }
            _clientRepository.Remove(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
