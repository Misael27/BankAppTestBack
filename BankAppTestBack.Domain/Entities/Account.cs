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

        private Account() { }
        public enum EAccountType { Ahorros, Corriente }

    }
}
