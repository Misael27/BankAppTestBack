using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Domain.Entities
{
    public class Person
    {
        public string Name { get; protected set; }
        public EGender Gender { get; protected set; }
        public DateTime Birthdate { get; protected set; }
        public string PersonId { get; protected set; }
        public string Address { get; protected set; }
        public string Phone { get; protected set; }
    }

    public enum EGender { M, F, Otro }

}
