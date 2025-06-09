using elaw_desenvolvedor_junior.Domain.Entities;
using elaw_desenvolvedor_junior.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace elaw_desenvolvedor_junior.Infrastructure.Persistence
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.Phone);

                entity.OwnsOne(c => c.Email, email =>
                {
                    email.Property(e => e.EmailAddress)
                        .IsRequired();

                    email.HasIndex(e => e.EmailAddress) 
                        .IsUnique();
                });

                entity.OwnsOne(c => c.Address, address =>
                {
                    address.Property(a => a.Street).IsRequired();
                    address.Property(a => a.Number).IsRequired();
                    address.Property(a => a.City).IsRequired();
                    address.Property(a => a.State).IsRequired();
                    address.Property(a => a.ZipCode).IsRequired();
                });
            });
        }
    }
}
