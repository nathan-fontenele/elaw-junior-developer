using elaw_desenvolvedor_junior.Domain.ValueObjects;

namespace elaw_desenvolvedor_junior.Application.Dtos
{
    public class GetClientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressDto Address { get; set; }
    }
}
