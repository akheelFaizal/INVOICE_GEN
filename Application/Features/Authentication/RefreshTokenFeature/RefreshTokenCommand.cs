using System;

namespace Application.Features.Authentication.RefreshTokenFeature;

public class RefreshTokenCommand
{
    public required string RefreshToken {get; set;} 
}
