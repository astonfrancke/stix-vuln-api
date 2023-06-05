using FluentValidation;

using StixVuln.Api.DTO.Vulnerability.Validation;
using StixVuln.Api.Extensions;
using StixVuln.Api.Mapping;

namespace StixVuln.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy =
            new SnakeCaseNamingPolicy();
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddMappings();
        services.AddValidatorsFromAssemblyContaining<CreateVulnerabilityValidator>();
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        // Todo add ProblemDetailsFactory
        return services;
    }
}
