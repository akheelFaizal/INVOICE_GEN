using System;

namespace InvoiceSystem.Identity.Application.Features.Authentication.Logout;

public class LogoutCommand
{
    public required string RefreshToken { get; set; }
}   
