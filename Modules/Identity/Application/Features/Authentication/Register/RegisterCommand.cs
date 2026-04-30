namespace InvoiceSystem.Identity.Application.Features.Authentication.Register;

public class RegisterCommand
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
