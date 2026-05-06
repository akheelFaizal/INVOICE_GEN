using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// Identity Namespaces
using InvoiceSystem.Identity.Application.Interfaces;
using InvoiceSystem.Identity.Application.Services;
using InvoiceSystem.Identity.Core.Interfaces;
using InvoiceSystem.Identity.Infrastructure.Repositories;
using InvoiceSystem.Identity.Infrastructure.Data;
using InvoiceSystem.Identity.Infrastructure.Services;
using InvoiceSystem.Identity.Application.Features.Authentication.Interfaces;
using InvoiceSystem.Identity.Application.Features.RoleOperations.Interfaces;
using InvoiceSystem.Identity.Application.Features.PermissionOperations.Interfaces;

// Clients Namespaces
using InvoiceSystem.Clients.Application.Interfaces;
using InvoiceSystem.Clients.Application.Services;
using InvoiceSystem.Clients.Core.Interfaces;
using InvoiceSystem.Clients.Infrastructure.Repositories;
using InvoiceSystem.Clients.Infrastructure.Data;

// Invoicing Namespaces
using InvoiceSystem.Invoicing.Application.Interfaces;
using InvoiceSystem.Invoicing.Application.Services;
using InvoiceSystem.Invoicing.Core.Interfaces;
using InvoiceSystem.Invoicing.Infrastructure.Repositories;
using InvoiceSystem.Invoicing.Infrastructure.Data;
using InvoiceSystem.Invoicing.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// DB Contexts
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ClientDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<InvoicingDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRoleInterface, RoleRepository>();
builder.Services.AddScoped<IPermissionInterface, PermissionRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

// Clients Services
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

// Invoicing Services
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceIntegrationService, InvoiceIntegrationService>();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "InvoiceSystem",
        ValidAudience = "InvoiceSystem",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? "YourSuperSecretKeyHere"))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger or other dev tools can be added here
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
