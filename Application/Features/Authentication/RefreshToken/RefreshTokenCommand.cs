using System;

namespace Application.Features.Authentication.RefreshToken;

public class RefreshTokenCommand
{
    public required string RefreshToken {get; set;} 
}
