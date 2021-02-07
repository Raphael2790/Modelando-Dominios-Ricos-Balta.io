using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

           AddNotifications(new Contract()
                .Requires()
                .HasMaxLen(firstName, 20, "Name.FirsName", "O primeiro nome deve conter no máximo 20 caracteres")
                .HasMaxLen(lastName, 20, "Name.LastName", "O sobrenome deve conter no máximo 20 caracteres")
           );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}