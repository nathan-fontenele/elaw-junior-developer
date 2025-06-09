using elaw_desenvolvedor_junior.Application.Dtos;

namespace elaw_desenvolvedor_junior.Application.Interfaces
{
    public interface IClientService
    {
        GetClientDto GetClientById(Guid id);

        List<GetClientDto> GetClients();

        GetClientDto CreateClient(CreateClientDto input);

        GetClientDto UpdateClient(Guid id, UpdateClientDto input);

        void DeleteClientById(Guid id);
    }
}
