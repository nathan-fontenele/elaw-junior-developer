using elaw_desenvolvedor_junior.Domain.Entities;

namespace elaw_desenvolvedor_junior.Domain.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(Guid id);
        void CreateClient(Client client);
        void UpdateClient(Guid id, Client client);
        void DeleteClient(Guid id);
    }
}
