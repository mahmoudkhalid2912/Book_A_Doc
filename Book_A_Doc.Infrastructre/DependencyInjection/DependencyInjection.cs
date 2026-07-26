using Book_A_Doc.Application.Services.Jwt_Service;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Infrastructre.JwtServices;
using Book_A_Doc.Infrastructre.JwtServices.OptionsClass;
using Book_A_Doc.Infrastructre.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

/// <summary>
/// Provides extension methods for registering Infrastructure layer services
/// such as the database context and ASP.NET Core Identity.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all infrastructure services required by the application.
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add DbContext with SQL Server provider
        services.AddDbContext<Book_A_Doc_Context>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("ConnectionString"));
        });

        // Add Identity services
        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            // SignIn settings.
            options.SignIn.RequireConfirmedEmail = true;
        });

        services.AddAuthConfig(configuration);

        return services;
    }

    private static IServiceCollection AddAuthConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSetting = configuration
            .GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>();

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<Book_A_Doc_Context>()
                .AddDefaultTokenProviders();

        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSetting?.Issuer,
                ValidAudience = jwtSetting?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSetting?.Key ?? string.Empty))
            };
        });

        return services;
    }
}