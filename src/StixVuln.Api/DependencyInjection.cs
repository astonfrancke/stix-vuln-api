using System.Text.Json;

using Microsoft.AspNetCore.Mvc.Infrastructure;

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
        // Todo add ProblemDetailsFactory
        return services;
    }
}
