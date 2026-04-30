using InvoiceSystem.Clients.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Clients.Infrastructure.Data;

public class ClientDbContext : DbContext
{
    public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
