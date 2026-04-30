using System;
using InvoiceSystem.Identity.Core.Entities;

namespace InvoiceSystem.Identity.Application.Features.Authentication.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
}
