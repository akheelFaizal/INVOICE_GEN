using InvoiceSystem.Clients.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceSystem.Clients.Infrastructure.Data;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(200);
        
        // Financial Safety: Strictly use decimal
        builder.Property(c => c.OutstandingBalance).HasPrecision(18, 2);
        builder.Property(c => c.TotalBilled).HasPrecision(18, 2);
    }
}
