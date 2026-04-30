using InvoiceSystem.Identity.Core.Entities;

namespace InvoiceSystem.Identity.Core.Interfaces;

public interface IUserRepository
{
    Task AddUser(User user);
    Task<User> GetUserByEmail(string email);
    Task<User> GetById(Guid Id);
}
