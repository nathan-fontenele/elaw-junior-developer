namespace elaw_desenvolvedor_junior.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }

        public Address()
        {
            
        }

        public Address(string street, string number, string city, string state, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentNullException("Rua é obrigatória.");
            if (string.IsNullOrWhiteSpace(number)) throw new ArgumentNullException("Número é obrigatório.");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentNullException("Cidade é obrigatória.");
            if (string.IsNullOrWhiteSpace(state)) throw new ArgumentNullException("Estado é obrigatório.");
            if (string.IsNullOrWhiteSpace(zipCode)) throw new ArgumentNullException("CEP é obrigatório.");

            Street = street;
            Number = number;
            City = city;
            State = state;
            ZipCode = zipCode;
        }
    }
}
