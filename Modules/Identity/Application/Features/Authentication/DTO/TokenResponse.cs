using System;

namespace InvoiceSystem.Identity.Application.Features.Authentication.DTO;

public class TokenResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
