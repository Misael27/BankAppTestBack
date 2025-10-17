using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Domain.Entities
{
    public class Movement
    {
        public long Id { get; set; }
        public DateTime Date { get; private set; } = DateTime.Now;
        public EMovementType Type { get; set; }
        public double Value { get; set; }
        public double Balance { get; set; }
        public long AccountId { get; set; }

        public virtual Account Account { get; set; }

        public bool IsValid()
        {
            if (Value <= 0)
            {
                return false;
            }
            if (AccountId <= 0)
            {
                return false;
            }
            return true;
        }

        public void AddBalance(double accountBalance)
        {
            Balance = Type switch
            {
                EMovementType.Retiro => accountBalance - Value,
                EMovementType.Deposito => accountBalance + Value,
                _ => throw new InvalidOperationException($"Unexpected value: {Type}")
            };
            Date = DateTime.UtcNow;
        }

        public bool IsBalanceValid()
        {
            return Balance >= 0;
        }

        public bool CanDebitToday(double todayDebitTotal, double maxLimit)
        {
            return Value + todayDebitTotal <= maxLimit;
        }

    }

    public enum EMovementType { Retiro, Deposito }

}
