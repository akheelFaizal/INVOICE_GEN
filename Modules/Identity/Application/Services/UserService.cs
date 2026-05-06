using InvoiceSystem.Identity.Application.DTOs;
using InvoiceSystem.Identity.Application.Interfaces;
using InvoiceSystem.Identity.Core.Interfaces;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Identity.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<UserResponse>>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsers();
        var response = users.Select(user => 
        {
            var roles = user.UserRoles?.Select(ur => ur.Role.Name) ?? new List<string>();
            return new UserResponse(user.Id, user.Email, user.Name, roles);
        });

        return Result<IEnumerable<UserResponse>>.SuccessResult(response);
    }

    public async Task<Result<UserResponse>> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
            return Result<UserResponse>.FailureResult("User not found.");

        // We might need to load roles here if GetById doesn't include them, 
        // but let's assume it does or handle it if it fails.
        var roles = user.UserRoles?.Select(ur => ur.Role.Name) ?? new List<string>();
        var response = new UserResponse(user.Id, user.Email, user.Name, roles);

        return Result<UserResponse>.SuccessResult(response);
    }

    public async Task<Result<UserResponse>> UpdateUserAsync(Guid id, UpdateUserRequest request)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
            return Result<UserResponse>.FailureResult("User not found.");

        user.Name = request.FullName;
        // Not updating email/password here, usually done via separate endpoints for security

        await _userRepository.UpdateUser(user);

        var roles = user.UserRoles?.Select(ur => ur.Role.Name) ?? new List<string>();
        var response = new UserResponse(user.Id, user.Email, user.Name, roles);

        return Result<UserResponse>.SuccessResult(response);
    }

    public async Task<Result> DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
            return Result.FailureResult("User not found.");

        await _userRepository.DeleteUser(user);
        return Result.SuccessResult();
    }
}
