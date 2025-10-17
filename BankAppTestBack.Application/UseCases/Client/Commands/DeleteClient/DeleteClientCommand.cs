using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.UseCases.Client.Commands.DeleteClient
{
    public record DeleteClientCommand(long ClientId) : IRequest<Unit>;
}
