using System;

namespace Application.Features.Authentication.Logout;

public class LogoutCommand
{
    public required string RefreshToken { get; set; }
}   