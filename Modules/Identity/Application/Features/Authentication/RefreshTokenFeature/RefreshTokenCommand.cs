using System;

namespace InvoiceSystem.Identity.Application.Features.Authentication.RefreshTokenFeature;

public class RefreshTokenCommand
{
    public required string RefreshToken {get; set;} 
}
