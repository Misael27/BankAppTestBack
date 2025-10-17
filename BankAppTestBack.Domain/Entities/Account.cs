using BankAppTestBack.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Domain.Entities
{
    public class Account
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public EAccountType Type { get; set; }
        public double InitBalance { get; set; }
        public bool State { get; set; }

        public long ClientId { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                return false;
            }

            if (InitBalance < 0)
            {
                return false;
            }

            if (ClientId <= 0)
            {
                return false;
            }

            return true;
        }

        public void Update(
            string? number = null,
            EAccountType? type = null,
            double? initBalance = null,
            bool? state = null,
            long? clientId = null
        )
        {
            Number = !string.IsNullOrWhiteSpace(number) ? number : Number;
            Type = type.HasValue ? type.Value : Type;
            InitBalance = initBalance.HasValue ? initBalance.Value : InitBalance;
            State = state.HasValue ? state.Value : State;
            ClientId = clientId.HasValue ? clientId.Value : ClientId;

            if (!IsValid())
                throw new DomainException("The update leaves the account in an invalid state");
        }
    }
    public enum EAccountType { Ahorros, Corriente }

}
