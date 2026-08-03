using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Infrastructre.Configurations;
using Book_A_Doc.Infrastructre.Persistence;
using Book_A_Doc.Infrastructre.Services.Authentication;
using Book_A_Doc.Infrastructre.Services.Authentication.JWT;
using Book_A_Doc.Infrastructre.Services.Identity;
using Book_A_Doc.Infrastructre.Services.Mail.Options;
using Book_A_Doc.Infrastructre.Services.Mail.Service;
using Book_A_Doc.Infrastructre.Services.RefreshTokens;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<Book_A_Doc_Context>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("ConnectionString"));
        });


        services.Configure<IdentityOptions>(options =>
        {
            // Password
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;


            // Lockout
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;


            // User
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            options.User.RequireUniqueEmail = true;


            // SignIn
            options.SignIn.RequireConfirmedEmail = true;
        });


        services.AddAuthConfig(configuration);

        services.AddHangfireConfig(configuration);

        services.AddMailConfig(configuration);

        services.AddApplicationSettings(configuration);

        services.AddInfrastructureServices();


        return services;
    }



    private static IServiceCollection AddAuthConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));


        var jwtSettings = configuration
            .GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>()
            ?? throw new InvalidOperationException(
                "JWT configuration is missing.");


        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<Book_A_Doc_Context>()
            .AddDefaultTokenProviders();



        services.AddSingleton<IJwtProvider, JwtProvider>();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;

            options.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;


            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,

                    ValidateAudience = true,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,


                    ValidIssuer = jwtSettings.Issuer,

                    ValidAudience = jwtSettings.Audience,


                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Key)),


                    ClockSkew = TimeSpan.Zero
                };
        });


        return services;
    }



    private static IServiceCollection AddMailConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MailOptions>(
            configuration.GetSection(MailOptions.SectionName));


        return services;
    }



    private static IServiceCollection AddApplicationSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ApplicationSettings>(
            configuration.GetSection(ApplicationSettings.SectionName));


        return services;
    }



    private static IServiceCollection AddHangfireConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHangfire(cfg => cfg

            .SetDataCompatibilityLevel(
                CompatibilityLevel.Version_180)

            .UseSimpleAssemblyNameTypeSerializer()

            .UseRecommendedSerializerSettings()

            .UseSqlServerStorage(
                configuration.GetConnectionString(
                    "HangfireConnection")));


        services.AddHangfireServer();


        return services;
    }



    private static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddScoped<ITokenEncoder, TokenEncoder>();

        services.AddScoped<IEmailTemplateService, EmailTemplateService>();

        services.AddScoped<IEmailService, EmailSender>();


        services.AddSingleton<IApplicationSettings, ApplicationSettingsProvider>();


        return services;
    }
}