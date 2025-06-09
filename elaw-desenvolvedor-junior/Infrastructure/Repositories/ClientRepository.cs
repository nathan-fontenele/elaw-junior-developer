using System;
using System.Collections.Generic;
using System.Linq;
using elaw_desenvolvedor_junior.Domain.Entities;
using elaw_desenvolvedor_junior.Domain.Interfaces;
using elaw_desenvolvedor_junior.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace elaw_desenvolvedor_junior.Infrastructure.Repositories
{
    internal class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _context.Clients.AsNoTracking().ToList();
        }

        public Client GetClientById(Guid id)
        {
            return _context.Clients.Find(id);
        }

        public void CreateClient(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            bool existingEmail = _context.Clients
               .AsNoTracking()
               .Any(c =>
                   c.Email.EmailAddress.Equals(client.Email.EmailAddress, StringComparison.OrdinalIgnoreCase)
                   && c.Id != client.Id
               );

            if (existingEmail)
                throw new ArgumentException($"The email '{client.Email.EmailAddress}' is already in use.");

            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void UpdateClient(Guid id, Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            var existing = _context.Clients.Find(id);
            if (existing == null)
                throw new InvalidOperationException($"Cliente com id {id} não encontrado.");

            existing.SetName(client.Name);
            existing.SetPhone(client.Phone);
            existing.SetEmail(client.Email);
            existing.SetAddress(client.Address);

            _context.SaveChanges();
        }


        public void DeleteClient(Guid id)
        {
            var existing = _context.Clients.Find(id);
            if (existing == null)
                throw new InvalidOperationException($"Cliente com id {id} não encontrado.");

            _context.Clients.Remove(existing);
            _context.SaveChanges();
        }
    }
}
