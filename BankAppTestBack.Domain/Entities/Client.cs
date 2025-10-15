using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Domain.Entities
{
    public class Client : Person
    {
        public long Id { get; set; }
        public string Password { get; private set; }
        public bool State { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

        private Client() { }

        public Client(string name, EGender gender, DateTime birthdate, string personId,
                      string address, string phone, string password, bool state)
        {
            Name = name;
            Gender = gender;
            Birthdate = birthdate;
            PersonId = personId;
            Address = address;
            Phone = phone;
            Password = password;
            State = state;
        }

        public bool IsValid()
        {
            return true;
        }

        public void EncryptPassword(string plainPassword)
        {
            Password = plainPassword;
        }

        public void Update(Client clientUpdate)
        {
        }
    }
}
