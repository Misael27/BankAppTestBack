using BankAppTestBack.Domain.Common;
using BankAppTestBack.Domain.Exceptions;
namespace BankAppTestBack.Domain.Entities
{
    public class Client : Person, ICommonDomain
    {
        public long Id { get; set; }
        public string Password { get; private set; }
        public bool State { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

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
        bool? state,
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
            State = state.HasValue ? state.Value : State;
            if (!IsValid()) throw new DomainException("INVALID_REQUEST");
        }
    }
}
