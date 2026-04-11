using System;
using Domain.Entities;

namespace Application.Features.Authentication.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
}
