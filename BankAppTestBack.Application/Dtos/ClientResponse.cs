using BankAppTestBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankAppTestBack.Application.Dtos
{
    public record ClientResponse(
        long Id,
        string Name,
        EGender Gender,
        string Birthdate,
        string PersonId,
        string Address,
        string Phone,
        bool State
    );
}
