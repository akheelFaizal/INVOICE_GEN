using System.Text;
using Application.Features.Authentication.Interfaces;
using Application.Features.Authentication.Login;
using Application.Features.Authentication.Logout;
using Application.Features.Authentication.RefreshTokenFeature;
using Application.Features.Authentication.Register;
using Application.Features.PermissionOperations.AddPermission;
using Application.Features.PermissionOperations.AssignPermission;
using Application.Features.PermissionOperations.GetPermission;
using Application.Features.PermissionOperations.Interfaces;
using Application.Features.RoleOperations.AssignRole;
using Application.Features.RoleOperations.DeleteRole;
using Application.Features.RoleOperations.GetRoles;
using Application.Features.RoleOperations.Interfaces;
using Application.Features.RoleOperations.RoleFeature;
using Application.Features.RoleOperations.UpdateRole;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRoleInterface, RoleRepository>();
builder.Services.AddScoped<IPermissionInterface, PermissionRepository>();

builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RefreshTokenHandler>();
builder.Services.AddScoped<LogoutHandler>();
builder.Services.AddScoped<RoleHandler>();
builder.Services.AddScoped<GetRoleHandler>();
builder.Services.AddScoped<UpdateRoleHandler>();
builder.Services.AddScoped<DeleteRoleHandler>();
builder.Services.AddScoped<AssignRoleHandler>();
builder.Services.AddScoped<AddPermissionHandler>();
builder.Services.AddScoped<GetPermissionHandler>();
builder.Services.AddScoped<AssignPermissionHandler>();


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
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateInvoice",
        policy => policy.RequireClaim("permission", "CreateInvoice"));

    options.AddPolicy("ViewReports",
        policy => policy.RequireClaim("permission", "ViewReports"));

    options.AddPolicy("ManageUsers",
        policy => policy.RequireClaim("permission", "ManageUsers"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();

