using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.Client.Commands.CreateClient
{
    public record ClientResponse(
        long Id,
        string Name,
        string PersonId,
        bool State
    );
}
