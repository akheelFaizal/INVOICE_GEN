using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Features.Authentication.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class TokenService : ITokenService
{

    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        if(user.UserRoles != null)
        {
            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                if (userRole.Role.RolePermissions != null)
                {
                    foreach (var rolePermission in userRole.Role.RolePermissions)
                    {
                        claims.Add(new Claim("permission", rolePermission.Permission.Name));
                    }
                }
            }
            
        }
        var secretKey = _configuration["Jwt:SecretKey"];

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "InvoiceSystem",
            audience: "InvoiceSystem",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }
   
}
