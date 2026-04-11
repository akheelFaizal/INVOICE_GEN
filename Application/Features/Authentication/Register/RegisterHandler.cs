using System.Runtime.InteropServices;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Helpers;

namespace Application.Features.Authentication.Register;

public class RegisterHandler
{   
    private readonly IUserRepository _userRepository;

    public RegisterHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<string> Handle(RegisterCommand command)
    {
        var existing = await _userRepository.GetUserByEmail(command.Email);
        if(existing != null)
        {
            return "User Exists!";
        }

        var user = new User
        {
          Name = command.Name,
          Email = command.Email,
          PasswordHash = PasswordHasher.HashPassword(command.Password)
        };

        await _userRepository.AddUser(user);
        return "User Registered succesfully!";
    }
}
