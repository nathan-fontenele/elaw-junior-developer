namespace elaw_desenvolvedor_junior.Application.Dtos
{
    public class UpdateClientDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public AddressDto? Address { get; set; }
    }
}
