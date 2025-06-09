using elaw_desenvolvedor_junior.Domain.ValueObjects;

namespace elaw_desenvolvedor_junior.Domain.Entities
{
    public class Client
    {
        internal Guid Id { get; private set; }
        internal string Name { get; private set; }
        internal Email Email { get; private set; }
        internal string? Phone { get; private set; }
        internal Address Address { get; private set; }

        public Client() { }

        public Client(string name, string email, string phone)
        {
            if(String.IsNullOrEmpty(name)) 
                throw new ArgumentNullException(nameof(name), "O campo Nome é obrigatório.");
            
            Id = Guid.NewGuid();
            Name = name;
            Email = new Email(email);
            Phone = phone;
        }
        
        public void SetAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address), "O endereço não pode ser nulo.");
                
            Address = address;
        }

        public void SetName(string name)
        {
            if (String.IsNullOrEmpty(name)) 
                throw new ArgumentNullException("Nome é obrigatório");

            Name = name;
        }

        public void SetEmail(Email email)
        {
            if(email == null)
                throw new ArgumentNullException(nameof(email), "Email é obrigatório");

            Email = email;
        }

        public void SetPhone(string? phone)
        {
            Phone = phone;
        }
    }
}
