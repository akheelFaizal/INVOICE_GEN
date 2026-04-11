using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task AddUser(User user);
    Task<User> GetUserByEmail(string email);
    Task<User> GetById(Guid Id);
}
