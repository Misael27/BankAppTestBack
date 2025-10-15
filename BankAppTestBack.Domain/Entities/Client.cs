using BankAppTestBack.Domain.Common;
using BankAppTestBack.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Domain.Entities
{
    public class Client : Person, ICommonDomain
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

        public void EncryptPassword(string plainPassword)
        {
            Password = plainPassword;
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(PersonId) ||
                string.IsNullOrWhiteSpace(Address) ||
                string.IsNullOrWhiteSpace(Phone) ||
                string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            if (Birthdate.Date > DateTime.UtcNow.Date)
            {
                return false;
            }

            return true;
        }

    public void Update(
        string? name,
        EGender? gender,
        DateTime? birthdate,
        string? personId,
        string? address,
        string? phone,
        string? hashedPassword = null
    )
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : Name;
            Gender = gender.HasValue ? gender.Value : Gender;
            Birthdate = birthdate.HasValue ? birthdate.Value : Birthdate;
            PersonId = !string.IsNullOrWhiteSpace(personId) ? personId : PersonId;
            Address = !string.IsNullOrWhiteSpace(address) ? address : Address;
            Phone = !string.IsNullOrWhiteSpace(phone) ? phone : Phone;
            if (!string.IsNullOrWhiteSpace(hashedPassword))
            {
                Password = hashedPassword;
            }

            if (!IsValid()) throw new DomainException("The update leaves the client in an invalid state");
        }
    }
}
