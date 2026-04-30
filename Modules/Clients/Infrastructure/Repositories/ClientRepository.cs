using InvoiceSystem.Clients.Core.Entities;
using InvoiceSystem.Clients.Core.Interfaces;
using InvoiceSystem.Clients.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Clients.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ClientDbContext _context;

    public ClientRepository(ClientDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(Guid id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task AddAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}
