using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using StixVuln.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using StixVuln.Core.Vulnerability.Interfaces;
using StixVuln.Infrastructure.Authenticaiton;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StixVuln.Core.Authentication.Interfaces;

namespace StixVuln.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddAuth(configuration)
            .AddPersistence(configuration);
        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("VulnerabilityDb")));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVulnerabiltiesRepository, VulnerabilitiesRepository>();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(
            defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8
                                .GetBytes(jwtSettings.Secret))
                    });

        return services;
    }
}
