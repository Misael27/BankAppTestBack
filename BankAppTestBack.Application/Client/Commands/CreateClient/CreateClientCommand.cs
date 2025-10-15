using BankAppTestBack.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.Client.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<ClientResponse>
    {
        public string Name { get; set; }
        public EGender Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string PersonId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool State { get; set; }
    }
}
