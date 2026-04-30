using InvoiceSystem.Identity.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Identity.Application.Interfaces;

public interface IUserService
{
    Task<Result<IEnumerable<UserResponse>>> GetAllUsersAsync();
    Task<Result<UserResponse>> GetUserByIdAsync(Guid id);
    Task<Result<UserResponse>> CreateUserAsync(RegisterRequest request);
    Task<Result> UpdateUserStatusAsync(Guid id, bool isActive);
    Task<Result<UserResponse>> UpdateUserAsync(Guid id, UpdateUserRequest request);
    Task<Result> DeleteUserAsync(Guid id);

}
