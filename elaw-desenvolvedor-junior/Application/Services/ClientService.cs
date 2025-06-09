using AutoMapper;
using elaw_desenvolvedor_junior.Application.Dtos;
using elaw_desenvolvedor_junior.Application.Interfaces;
using elaw_desenvolvedor_junior.Domain.Entities;
using elaw_desenvolvedor_junior.Domain.Interfaces;
using elaw_desenvolvedor_junior.Domain.ValueObjects;

namespace elaw_desenvolvedor_junior.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _repository = clientRepository;
            _mapper = mapper;
        }


        public List<GetClientDto> GetClients()
        {
            var client = _repository.GetAllClients();
            if (client == null) throw new InvalidOperationException("Sem usuários cadastrados");

            return _mapper.Map<List<GetClientDto>>(client);
        }

        public GetClientDto CreateClient(CreateClientDto input)
        {
            var client = new Client(input.Name, input.Email, input.Phone);

            if (input.Address != null)
            {
                var address = _mapper.Map<Address>(input.Address);
                client.SetAddress(address);
            }

            _repository.CreateClient(client);

            return _mapper.Map<GetClientDto>(client);
        }

        GetClientDto IClientService.GetClientById(Guid id)
        {
            var client = _repository.GetClientById(id);
            if (client == null)
            {
                throw new InvalidOperationException("Usuário inexistente");
            }

            return _mapper.Map<GetClientDto>(client);
        }

        public void DeleteClientById(Guid id)
        {
            if (id == null) throw new ArgumentNullException("O Id não pode ser nulo.");

            _repository.DeleteClient(id);
        }

        public GetClientDto UpdateClient(Guid id, UpdateClientDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var existing = _repository.GetClientById(id);
            if (existing == null) return null;

            if (!string.IsNullOrWhiteSpace(input.Name))
                existing.SetName(input.Name);

            if (!string.IsNullOrWhiteSpace(input.Email))
                existing.SetEmail(new Email(input.Email));

            if (input.Phone != null)
                existing.SetPhone(input.Phone);

            if (input.Address != null)
            {
                var address = _mapper.Map<Address>(input.Address);
                existing.SetAddress(address);
            }

            _repository.UpdateClient(id, existing);
            return _mapper.Map<GetClientDto>(existing);
        }

    }
}
